using System.Net.Http;
using System.Threading.Tasks;
using MobileBalanceHandler.Models;
using MobileBalanceHandler.Services.RequestHandlerServices;

namespace MobileBalanceHandler.Services.ServicesComposer
{
    public interface IServicesComposer
    {
        public Task<HttpResponseMessage> ComposeServices(PaymentData paymentData);

        public Task<HttpResponseMessage> SendRequest(IRequestHandler requestHandler, PaymentData paymentData,
            string url);
    }
}