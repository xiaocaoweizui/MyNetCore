using System.Net.Http;
using System.Threading.Tasks;

namespace MyNetCoreServer.Client
{
    public class NamedOrderServiceClient
    {
        IHttpClientFactory _httpClientFactory;

        const string _clientName = "OrderClient1";

        public NamedOrderServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Get()
        {

            //根据名称传递
            var client = _httpClientFactory.CreateClient(_clientName);

            //使用client发送HTTP请求
            return await client.GetStringAsync("/OrderService/Get");
        }
    }
}
