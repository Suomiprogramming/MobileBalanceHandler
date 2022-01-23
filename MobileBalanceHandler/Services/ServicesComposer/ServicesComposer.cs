using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private static HttpContext HttpContext => new HttpContextAccessor().HttpContext;

        public ServicesComposer(ICarrierService carrierService, IPaymentService paymentService)
        {
            _carrierService = carrierService;
            _paymentService = paymentService;
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
                            Content = new StringContent("Приносим извинения, произошла ошибка при отправке запроса!")
                        };
                    }
                }
            }
            
            ErrorLogger.Error($"Произошла ошибка при запросе на пополнение по номеру: {paymentData.PhoneNumber} " +
                              $"на сумму {paymentData.Sum} с request id: {HttpContext.Response.Headers["RequestId"]} так, как оператор по префиксу {prefix} не был найден");

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("Оператора по такому префиксу не существует!")
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
                }
                
                return response;
            }

            ErrorLogger.Error($"Произошла ошибка при запросе на пополнение по номеру: {paymentData.PhoneNumber} " +
                              $"на сумму {paymentData.Sum} с request id: {HttpContext.Response.Headers["RequestId"]} так, как произошла ошибка при отправке POST-запроса по адресу: {url}");
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("Запрос не был отправлен, произошла ошибка на сервере!")
            };
        }
    }
}