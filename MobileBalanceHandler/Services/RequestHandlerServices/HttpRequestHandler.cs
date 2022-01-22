using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MobileBalanceHandler.Services.RequestHandlerServices
{
    public class HttpRequestHandler : IRequestHandler
    {
        private readonly HttpClient _httpClient;

        public HttpRequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T model)
        {
            _httpClient.Timeout = TimeSpan.FromMinutes(2);
            var json = JsonSerializer.Serialize(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(url, data);
        }
    }
}