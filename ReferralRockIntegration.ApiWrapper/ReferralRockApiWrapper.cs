using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models;
using System.Collections;
using System.Text;
using System.Text.Json;

namespace ReferralRockIntegration.ApiWrapper
{
    public class ReferralRockApiWrapper : IReferralRockApiWrapper
    {
        private readonly HttpClient _httpClient;

        public ReferralRockApiWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAllMembersAsync<T>(MemberRequestParameter memberRequest)
            where T : IList
        {
            StringBuilder urlBuilder = new();

            urlBuilder.Append("/members");
            urlBuilder.Append($"?programId={memberRequest.ProgramId}");
            urlBuilder.Append($"&query={memberRequest.Query}");
            urlBuilder.Append($"&showDisabled={memberRequest.ShowDisabled}");
            urlBuilder.Append($"&showDisabled={memberRequest.ShowDisabled}");
            urlBuilder.Append($"&sort={memberRequest.Sort}");
            urlBuilder.Append($"&dateFrom={memberRequest.DateFrom}");
            urlBuilder.Append($"&dateTo={memberRequest.DateTo}");
            urlBuilder.Append($"&offset={memberRequest.OffSet}");
            urlBuilder.Append($"&count={memberRequest.Count}");

            var response = await _httpClient.GetAsync(urlBuilder.ToString());

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(response.ReasonPhrase);

            var httpResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(httpResponse);
        }
    }
}