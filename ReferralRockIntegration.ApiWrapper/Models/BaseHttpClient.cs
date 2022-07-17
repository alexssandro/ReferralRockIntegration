using System.Text.Json;

namespace ReferralRockIntegration.ApiWrapper.Models
{
    public abstract class BaseHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public BaseHttpClient(IHttpClientFactory httpClientFactory, string clientName)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient(clientName);
        }

        protected async Task<T> GetAsync<T>(string url) where T : class
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(response.ReasonPhrase);

            var httpResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(httpResponse);
        }
    }
}
