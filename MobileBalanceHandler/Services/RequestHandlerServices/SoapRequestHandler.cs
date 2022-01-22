using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MobileBalanceHandler.Services.RequestHandlerServices
{
    public class SoapRequestHandler : IRequestHandler
    {
        private readonly HttpClient _httpClient;

        public SoapRequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T model)
        {
            _httpClient.Timeout = TimeSpan.FromMinutes(2);
            string xml;
            using (StringWriter stringWriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringWriter, model);
                xml = stringWriter.ToString();
                xml = xml.Replace("utf-16", "utf-8");
            }
                                
            var data = new StringContent(xml, Encoding.UTF8, "application/xml");
                                

            return await _httpClient.PostAsync(url, data);
        }
    }
}