using System.ComponentModel.DataAnnotations;

namespace MobileBalanceHandler.Models
{
    public class PaymentData
    {
        [Required(ErrorMessage = "Введите номер!")]
        [RegularExpression(@"^\+7\d{10}", ErrorMessage = "Введите 11-значный номер, начиная с +7! Например, +77071234567")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Введите сумму!")]
        [Range(1, 1000000, ErrorMessage = "Неверно введена сумма! Принимаются значения от 1 до 1000000 тенге.")]
        public decimal Sum { get; set; }
    }
}