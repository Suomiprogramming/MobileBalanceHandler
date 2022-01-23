using System.Linq;
using Microsoft.EntityFrameworkCore;
using MobileBalanceHandler.Models;
using MobileBalanceHandler.Models.Data;
using MobileBalanceHandler.Services.PaymentServices;
using Xunit;

namespace MobileBalanceHandlerTests.Unit
{
    public class PaymentServiceTest
    {
        private MobileBalanceContext _context;
        private IPaymentService _paymentService;
        public PaymentServiceTest()
        {
            var options = new DbContextOptionsBuilder<MobileBalanceContext>()
                .UseInMemoryDatabase("PaymentServiceTestDb")
                .Options;
            _context = new MobileBalanceContext(options);
            _context.Database.EnsureDeleted();
            _paymentService = new PaymentService(_context);
        }

        [Fact]
        public void AddPayment_Adds_Payment()
        {
            var paymentData = new PaymentData()
            {
                PhoneNumber = "+77074442211",
                Sum = 2500,
            };
            _paymentService.AddPayment(paymentData);
            var payment = _context.Payments.FirstOrDefault(p => p.PhoneNumber == paymentData.PhoneNumber);
            Assert.NotNull(payment);
            Assert.Equal(paymentData.PhoneNumber, payment.PhoneNumber);
            Assert.Equal(paymentData.Sum, payment.Sum);
        }
    }
}