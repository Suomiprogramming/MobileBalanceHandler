using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using MobileBalanceHandler.Models;

namespace MobileBalanceHandler.Services.CarrierServices
{
    public class CarrierService : ICarrierService
    {
        private readonly IOptionsSnapshot<List<Carrier>> _config;

        public CarrierService(IOptionsSnapshot<List<Carrier>> config)
        {
            _config = config;
        }

        public string GetPrefix(string phoneNumber)
        {
            return phoneNumber.Trim().Substring(2, 3);
        }

        public Carrier GetByPrefix(string prefix)
        {
            var carriers = _config.Value;
            var carrier = carriers.FirstOrDefault(c => c.Prefix.Any(p => p.Contains(prefix)));
            return carrier;
        }
    }
}