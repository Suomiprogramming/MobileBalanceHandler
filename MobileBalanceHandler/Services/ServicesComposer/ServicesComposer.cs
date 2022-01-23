using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using MobileBalanceHandler.Models;
using MobileBalanceHandler.Services.CarrierServices;
using MobileBalanceHandler.Services.PaymentServices;
using MobileBalanceHandler.Services.RequestHandlerServices;
using NLog;

namespace MobileBalanceHandler.Services.ServicesComposer
{
    public class ServicesComposer : IServicesComposer
    {
        private readonly ICarrierService _carrierService;
        private readonly IPaymentService _paymentService;
        private static readonly Logger ErrorLogger = LogManager.GetLogger("errorRules");
        private readonly IStringLocalizer<Localization.Localization> _localizer;
        private static HttpContext HttpContext => new HttpContextAccessor().HttpContext;

        public ServicesComposer(
            ICarrierService carrierService, 
            IPaymentService paymentService, 
            IStringLocalizer<Localization.Localization> localizer)
        {
            _carrierService = carrierService;
            _paymentService = paymentService;
            _localizer = localizer;
        }

        public async Task<HttpResponseMessage> ComposeServices(PaymentData paymentData)
        {
            var prefix = _carrierService.GetPrefix(paymentData.PhoneNumber);
            var carrier = _carrierService.GetByPrefix(prefix);
            if (carrier != null)
            {
                switch (carrier.RequestType.Trim().ToUpper())
                {
                    case "SOAP":
                    {
                        return await SendRequest(new SoapRequestHandler(new HttpClient()), paymentData, carrier.Url);
                    }
                    case "HTTP":
                    {
                        return await SendRequest(new HttpRequestHandler(new HttpClient()), paymentData,
                            carrier.Url);
                    }
                    default:
                    {
                        ErrorLogger.Error($"Произошла ошибка при запросе на пополнение по номеру: {paymentData.PhoneNumber} " +
                                          $"на сумму {paymentData.Sum} с request id: {HttpContext.Response.Headers["RequestId"]} так, как запрос с типом {carrier.RequestType} не может быть обработан");
                        return new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Content = new StringContent(_localizer.GetString("reqTypeError"))
                        };
                    }
                }
            }
            
            ErrorLogger.Error($"Произошла ошибка при запросе на пополнение по номеру: {paymentData.PhoneNumber} " +
                              $"на сумму {paymentData.Sum} с request id: {HttpContext.Response.Headers["RequestId"]} так, как оператор по префиксу {prefix} не был найден");

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(_localizer.GetString("prefixError"))
            };
        }

        public async Task<HttpResponseMessage> SendRequest(IRequestHandler requestHandler, PaymentData paymentData, string url)
        {
            var response = await requestHandler.PostAsync(url, paymentData);
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    _paymentService.AddPayment(paymentData);
                }
                else
                {
                    ErrorLogger.Error($"Произошла ошибка при запросе на пополнение по номеру: {paymentData.PhoneNumber} " +
                                      $"на сумму {paymentData.Sum} с request id: {HttpContext.Response.Headers["RequestId"]} так, как при отправке POST-запроса по адресу: {url} поступил статус код отличный от 200 с содержимым контента: {await response.Content.ReadAsStringAsync()}");
                    response.Content = new StringContent(_localizer.GetString("failedRequest"));
                }
                
                return response;
            }

            ErrorLogger.Error($"Произошла ошибка при запросе на пополнение по номеру: {paymentData.PhoneNumber} " +
                              $"на сумму {paymentData.Sum} с request id: {HttpContext.Response.Headers["RequestId"]} так, как произошла ошибка при отправке POST-запроса по адресу: {url}");
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(_localizer.GetString("responseError"))
            };
        }
    }
}