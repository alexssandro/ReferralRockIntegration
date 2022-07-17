using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ReferralRockIntegration.ApiWrapper.Models
{
    public abstract class BaseHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public BaseHttpClient(IHttpClientFactory httpClientFactory,
                              string clientName,
                              ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient(clientName);
            _logger = logger;
        }

        protected async Task<T> GetAsync<T>(string url) where T : class
        {
            var response = await _httpClient.GetAsync(url);

            var httpResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to read response from url {url} and response {httpResponse}", url, httpResponse);
                return default;
            }

            return JsonSerializer.Deserialize<T>(httpResponse);
        }
    }
}
