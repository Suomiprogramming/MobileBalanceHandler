using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MobileBalanceHandler.Models;
using MobileBalanceHandler.Services.CarrierServices;
using MobileBalanceHandler.Services.PaymentServices;
using MobileBalanceHandler.Services.RequestHandlerServices;

namespace MobileBalanceHandler.Services.ServicesComposer
{
    public class ServicesComposer : IServicesComposer
    {
        private readonly ICarrierService _carrierService;
        private readonly IPaymentService _paymentService;

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
                        return new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Content = new StringContent("Приносим извинения, произошла ошибка при отправке запроса!")
                        };
                    }
                }
            }

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

                return response;
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("Запрос не был отправлен, произошла ошибка на сервере!")
            };
        }
    }
}