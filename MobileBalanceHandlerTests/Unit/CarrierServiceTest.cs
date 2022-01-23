using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MobileBalanceHandler.Models;
using MobileBalanceHandler.Services.CarrierServices;
using Moq;
using Xunit;

namespace MobileBalanceHandlerTests.Unit
{
    public class CarrierServiceTest
    {
        private readonly ICarrierService _carrierService;
        
        public CarrierServiceTest()
        {
            var carriers = new List<Carrier>()
            {
                new Carrier()
                {
                    Name = "Altel",
                    Prefix = new []{"700", "708"},
                    RequestType = "SOAP",
                    Url = "https://altel.kz/addCash"
                },
                new Carrier()
                {
                    Name = "Beeline",
                    Prefix = new []{"705", "777"},
                    RequestType = "HTTP",
                    Url = "https://beeline.kz/addMoney"
                }
            };
            var mock = new Mock<IOptionsSnapshot<List<Carrier>>>();
            mock.Setup(m => m.Value).Returns(carriers);
            _carrierService = new CarrierService(mock.Object);
        }
        
        [Fact]
        public void GetPrefix_Returns_Prefix()
        {
            var firstPrefix = _carrierService.GetPrefix("+77051112244");
            var secondPrefix = _carrierService.GetPrefix("+77004441122");
            Assert.Equal("705", firstPrefix);
            Assert.Equal("700", secondPrefix);
        }

        [Fact]
        public void GetByPrefix_Returns_Carrier()
        {
            var firstCarrier = _carrierService.GetByPrefix("708");
            var secondCarrier = _carrierService.GetByPrefix("777");
            var nullCarrier = _carrierService.GetByPrefix("800");
            Assert.Equal("Altel", firstCarrier.Name);
            Assert.Equal(2, firstCarrier.Prefix.Length);
            Assert.Equal("https://altel.kz/addCash", firstCarrier.Url);
            Assert.Equal("SOAP", firstCarrier.RequestType);
            Assert.Equal("Beeline", secondCarrier.Name);
            Assert.Equal(2, secondCarrier.Prefix.Length);
            Assert.Equal("https://beeline.kz/addMoney", secondCarrier.Url);
            Assert.Equal("HTTP", secondCarrier.RequestType);
            Assert.Null(nullCarrier);
        }
    }
}