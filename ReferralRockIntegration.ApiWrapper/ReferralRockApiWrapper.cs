using Microsoft.Extensions.Configuration;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ReferralRockIntegration.ApiWrapper
{
    public class ReferralRockApiWrapper : BaseHttpClient, IReferralRockApiWrapper
    {
        private readonly ReferralRockConfiguration _referralRockConfiguration;

        public ReferralRockApiWrapper(IHttpClientFactory httpClientFactory,
                                        ReferralRockConfiguration referralRockConfiguration)
            : base(httpClientFactory, "ReferralRock")
        {
            _referralRockConfiguration = referralRockConfiguration;
        }

        public async Task<MemberResponse> GetAllMembersAsync(MemberRequestParameter memberRequest)
        {
            StringBuilder urlBuilder = new();

            urlBuilder.Append("/api/members");
            urlBuilder.Append($"?programId={_referralRockConfiguration.ProgramId}");

            if (!string.IsNullOrEmpty(memberRequest.Query))
                urlBuilder.Append($"&query={memberRequest.Query}");
            if (memberRequest.ShowDisabled.HasValue)
                urlBuilder.Append($"&showDisabled={memberRequest.ShowDisabled.Value}");
            if (!string.IsNullOrEmpty(memberRequest.Sort))
                urlBuilder.Append($"&sort={memberRequest.Sort}");
            if (memberRequest.DateFrom.HasValue)
                urlBuilder.Append($"&dateFrom={memberRequest.DateFrom.Value}");
            if (memberRequest.DateTo.HasValue)
                urlBuilder.Append($"&dateTo={memberRequest.DateTo.Value}");
            if (memberRequest.OffSet.HasValue)
                urlBuilder.Append($"&offset={memberRequest.OffSet}");
            if (memberRequest.Count.HasValue)
                urlBuilder.Append($"&count={memberRequest.Count}");

            return await GetAsync<MemberResponse>(urlBuilder.ToString());
        }
    }
}