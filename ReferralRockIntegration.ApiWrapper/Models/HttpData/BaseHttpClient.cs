﻿using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace ReferralRockIntegration.ApiWrapper.Models.HttpData
{
    public abstract class BaseHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        private readonly string _clientName;

        public BaseHttpClient(IHttpClientFactory httpClientFactory,
                              string clientName,
                              ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _clientName = clientName;
        }

        protected async Task<T> GetAsync<T>(string url) where T : class
        {
            var httpClient = _httpClientFactory.CreateClient(_clientName);
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await GetBodyResponse(response);
                _logger.LogError("Failed to read response from url {url} and response {httpResponse}", url, responseBody);
                return default;
            }

            return await CreateResponseData<T>(response);
        }

        protected async Task<T> PostAsync<T>(string url, object data) where T : class
        {
            var content = CreateRequestPayload(data);
            var httpClient = _httpClientFactory.CreateClient(_clientName);
            var response = await httpClient.PostAsync(url, content);

            CheckResponseResult(response);

            return await CreateResponseData<T>(response);
        }

        protected async Task<T> PutAsync<T>(string url, object data) where T : class
        {
            var content = CreateRequestPayload(data);
            var httpClient = _httpClientFactory.CreateClient(_clientName);
            var response = await httpClient.PutAsync(url, content);

            CheckResponseResult(response);

            return await CreateResponseData<T>(response);
        }

        protected async Task<T> PutAsync<T>(string url) where T : class
        {
            var httpClient = _httpClientFactory.CreateClient(_clientName);
            var response = await httpClient.DeleteAsync(url);

            CheckResponseResult(response);

            return await CreateResponseData<T>(response);
        }

        private static StringContent CreateRequestPayload(object data)
        {
            string requestPayload = JsonSerializer.Serialize(data);
            var content = new StringContent(requestPayload, Encoding.UTF8, "application/json");
            return content;
        }

        private static async Task<string> GetBodyResponse(HttpResponseMessage response)
        {
            var bodyResponse = await response.Content.ReadAsStringAsync();
            return bodyResponse;
        }

        private static async Task<T> CreateResponseData<T>(HttpResponseMessage response) where T : class
        {
            var bodyResponse = await GetBodyResponse(response);
            return JsonSerializer.Deserialize<T>(bodyResponse);
        }

        private static void CheckResponseResult(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}
