using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MobileBalanceHandler.Models;
using MobileBalanceHandler.Services.RequestHandlerServices;
using Moq;
using Moq.Protected;
using Xunit;

namespace MobileBalanceHandlerTests.Unit
{
    public class HttpRequestHandlerTest
    {
        private HttpClient _httpClient;
        private IRequestHandler _requestHandler;
        
        public HttpRequestHandlerTest()
        {
            var paymentData = new PaymentData()
            {
                PhoneNumber = "+77014445544",
                Sum = 1900
            };

            var json = JsonSerializer.Serialize(paymentData);
            var url = "http://localhost:8888";
            var httpResponseMessage = new HttpResponseMessage();
            httpResponseMessage.StatusCode = HttpStatusCode.OK;
            httpResponseMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var mock = new Mock<HttpMessageHandler>();
            mock.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);
            _httpClient = new HttpClient(mock.Object);
            _requestHandler = new HttpRequestHandler(_httpClient);
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
            var deserializedContent = JsonSerializer.Deserialize<PaymentData>(content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(paymentData.Sum, deserializedContent.Sum);
            Assert.Equal(paymentData.PhoneNumber, deserializedContent.PhoneNumber);
        }
    }
}