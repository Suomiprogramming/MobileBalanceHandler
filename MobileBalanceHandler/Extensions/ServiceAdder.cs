using Microsoft.Extensions.DependencyInjection;
using MobileBalanceHandler.Services.CarrierServices;
using MobileBalanceHandler.Services.PaymentServices;
using MobileBalanceHandler.Services.RequestHandlerServices;
using MobileBalanceHandler.Services.ServicesComposer;

namespace MobileBalanceHandler.Extensions
{
    public static class ServiceAdder
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler, SoapRequestHandler>();
            services.AddTransient<IRequestHandler, HttpRequestHandler>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<ICarrierService, CarrierService>();
            services.AddTransient<IServicesComposer, ServicesComposer>();
            services.AddHttpContextAccessor();
        }
    }
}