using Microsoft.Extensions.Logging;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Configuration;
using ReferralRockIntegration.ApiWrapper.Models.HttpData;
using ReferralRockIntegration.ApiWrapper.Models.Member;
using System.Text;

namespace ReferralRockIntegration.ApiWrapper
{
    public class MemberRepository : BaseHttpClient, IMemberRepository
    {
        private readonly ReferralRockConfiguration _referralRockConfiguration;

        public MemberRepository(IHttpClientFactory httpClientFactory,
                                        ReferralRockConfiguration referralRockConfiguration,
                                        ILogger<MemberRepository> logger)
            : base(httpClientFactory, "ReferralRock", logger)
        {
            _referralRockConfiguration = referralRockConfiguration;
        }

        public async Task<MemberResponse> SearchAsync(MemberRequestParameter memberRequest)
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

        public async Task<Member> GetByIdAsync(string id)
        {
            var memberResponse = await SearchAsync(new MemberRequestParameter { Query = id });

            if (memberResponse == null || !memberResponse.Members.Any())
                return null;

            return memberResponse.Members.FirstOrDefault();
        }
    }
}