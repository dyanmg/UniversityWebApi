using System.Net.Http.Json;

namespace RestClient
{
    internal class RestClient(IHttpClientFactory _httpClientFactory)
    {
        public async Task<T?> Get<T>(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();
            return await httpClient.GetFromJsonAsync<T>(url);
        }
    }
}
