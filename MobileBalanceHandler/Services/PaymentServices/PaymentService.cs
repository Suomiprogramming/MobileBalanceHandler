using Microsoft.AspNetCore.Http;
using MobileBalanceHandler.Models;
using MobileBalanceHandler.Models.Data;
using NLog;

namespace MobileBalanceHandler.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly MobileBalanceContext _context;
        private static readonly Logger InfoLogger = LogManager.GetLogger("infoRules");
        private static HttpContext HttpContext => new HttpContextAccessor().HttpContext;

        public PaymentService(MobileBalanceContext context)
        {
            _context = context;
        }

        public void AddPayment(PaymentData paymentData)
        {
            Payment payment = new Payment()
            {
                Sum = paymentData.Sum,
                PhoneNumber = paymentData.PhoneNumber
            };
            _context.Payments.Add(payment);
            _context.SaveChanges();
            if (HttpContext != null && HttpContext.Response.Headers.ContainsKey("RequestId"))
            {
                InfoLogger.Info($"Платеж по номеру {payment.PhoneNumber} на сумму {payment.Sum} , проведенный в {payment.PaymentDate}, с request id: {HttpContext.Response.Headers["RequestId"]} сохранен в базе под id {payment.Id}");
            }
        }
    }
}