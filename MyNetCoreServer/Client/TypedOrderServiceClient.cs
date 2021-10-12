using System.Net.Http;
using System.Threading.Tasks;

namespace MyNetCoreServer.Client
{
    public class TypedOrderServiceClient
    {
        HttpClient _httpClient;


        public TypedOrderServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Get()
        {
            //使用client发送HTTP请求
            return await _httpClient.GetStringAsync("/WeatherForecast");
        }
    }
}
