using System.Net.Http;
using System.Threading.Tasks;

namespace MobileBalanceHandler.Services.RequestHandlerServices
{
    public interface IRequestHandler
    {
        public Task<HttpResponseMessage> PostAsync<T>(string url, T model);
    }
}