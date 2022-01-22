using MobileBalanceHandler.Models;

namespace MobileBalanceHandler.Services.PaymentServices
{
    public interface IPaymentService
    {
        public void AddPayment(PaymentData paymentData);
    }
}