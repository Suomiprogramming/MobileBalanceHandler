using System;

namespace MobileBalanceHandler.Models
{
    public class Payment
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Sum { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}