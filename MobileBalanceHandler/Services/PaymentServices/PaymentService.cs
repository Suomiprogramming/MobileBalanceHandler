using System;
using MobileBalanceHandler.Models;
using MobileBalanceHandler.Models.Data;

namespace MobileBalanceHandler.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly MobileBalanceContext _context;

        public PaymentService(MobileBalanceContext context)
        {
            _context = context;
        }

        public void AddPayment(PaymentData paymentData)
        {
            Payment payment = new Payment()
            {
                Id = Guid.NewGuid().ToString(),
                PaymentDate = DateTime.Now,
                Sum = paymentData.Sum,
                PhoneNumber = paymentData.PhoneNumber
            };
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }
    }
}