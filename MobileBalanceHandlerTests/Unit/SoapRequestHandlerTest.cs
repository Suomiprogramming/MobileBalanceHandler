using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MobileBalanceHandler.Models;
using MobileBalanceHandler.Services.RequestHandlerServices;
using Moq;
using Moq.Protected;
using Xunit;

namespace MobileBalanceHandlerTests.Unit
{
    public class SoapRequestHandlerTest
    {
        private HttpClient _httpClient;
        private IRequestHandler _requestHandler;
        
        public SoapRequestHandlerTest()
        {
            var paymentData = new PaymentData()
            {
                PhoneNumber = "+77014445544",
                Sum = 1900
            };

            string xml;
            using (StringWriter stringWriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(PaymentData));
                serializer.Serialize(stringWriter, paymentData);
                xml = stringWriter.ToString();
                xml = xml.Replace("utf-16", "utf-8");
            }
            var url = "http://localhost:8888";
            var httpResponseMessage = new HttpResponseMessage();
            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            httpResponseMessage.Content = new StringContent(xml, Encoding.UTF8, "application/json");
            var mock = new Mock<HttpMessageHandler>();
            mock.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);
            _httpClient = new HttpClient(mock.Object);
            _requestHandler = new SoapRequestHandler(_httpClient);
        }
        
        [Fact]
        public async void PostAsync_Returns_Response_Ok()
        {
            var paymentData = new PaymentData()
            {
                PhoneNumber = "+77014445544",
                Sum = 1900
            };
            var response = await _requestHandler.PostAsync<PaymentData>("http://localhost:8888", paymentData);
            var content = await response.Content.ReadAsStringAsync();
            var deserializedContent = new PaymentData();
            using (TextReader reader = new StringReader(content))
            {
                var serializer = new XmlSerializer(typeof(PaymentData));
                deserializedContent = (PaymentData) serializer.Deserialize(reader);
            }
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(paymentData.Sum, deserializedContent.Sum);
            Assert.Equal(paymentData.PhoneNumber, deserializedContent.PhoneNumber);
        }
    }
}