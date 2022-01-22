using MobileBalanceHandler.Models;

namespace MobileBalanceHandler.Services.CarrierServices
{
    public interface ICarrierService
    {
        public string GetPrefix(string phoneNumber);
        public Carrier GetByPrefix(string prefix);
    }
}