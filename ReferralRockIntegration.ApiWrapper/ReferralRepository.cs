﻿using Microsoft.Extensions.Logging;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Configuration;
using ReferralRockIntegration.ApiWrapper.Models.HttpData;
using ReferralRockIntegration.ApiWrapper.Models.Referral;
using System.Text;

namespace ReferralRockIntegration.ApiWrapper
{
    public class ReferralRepository : BaseHttpClient, IReferralRepository
    {
        private readonly ReferralRockConfiguration _referralRockConfiguration;

        public ReferralRepository(IHttpClientFactory httpClientFactory,
                                  ReferralRockConfiguration referralRockConfiguration,
                                  ILogger<ReferralRepository> logger)
            : base(httpClientFactory, "ReferralRock", logger)
        {
            _referralRockConfiguration = referralRockConfiguration;
        }

        public async Task<ReferralResponse> SearchAsync(ReferralRequestParameter memberRequest)
        {
            StringBuilder urlBuilder = new();

            urlBuilder.Append("/api/referrals");
            urlBuilder.Append($"?programId={_referralRockConfiguration.ProgramId}");

            if (!string.IsNullOrEmpty(memberRequest.Query))
                urlBuilder.Append($"&query={memberRequest.Query}");
            if (!string.IsNullOrEmpty(memberRequest.MemberId))
                urlBuilder.Append($"&memberId={memberRequest.MemberId}");
            if (!string.IsNullOrEmpty(memberRequest.Sort))
                urlBuilder.Append($"&sort={memberRequest.Sort}");
            if (memberRequest.DateFrom.HasValue)
                urlBuilder.Append($"&dateFrom={memberRequest.DateFrom.Value}");
            if (memberRequest.DateTo.HasValue)
                urlBuilder.Append($"&dateTo={memberRequest.DateTo.Value}");
            if (!string.IsNullOrEmpty(memberRequest.Status))
                urlBuilder.Append($"&status={memberRequest.Status}");
            if (memberRequest.OffSet.HasValue)
                urlBuilder.Append($"&offset={memberRequest.OffSet}");
            if (memberRequest.Count.HasValue)
                urlBuilder.Append($"&count={memberRequest.Count}");

            return await GetAsync<ReferralResponse>(urlBuilder.ToString());
        }

        public async Task<Referral> GetByIdAsync(string id)
        {
            string url = $"/api/referral/getsingle?referralQuery={id}";
            return await GetAsync<Referral>(url);
        }

        public async Task<object> AddAsync()
        {
            StringBuilder urlBuilder = new();
            urlBuilder.Append("/api/referrals");

            return await PostAsync<object>(urlBuilder.ToString(), new object());
        }

        public async Task<object> EditAsync()
        {
            StringBuilder urlBuilder = new();
            urlBuilder.Append("/api/referral/update");
            return await PostAsync<object>(urlBuilder.ToString(), new object());
        }

        public async Task<object> RemoveAsync(string id)
        {
            StringBuilder urlBuilder = new();
            urlBuilder.Append("/api/referral/remove");
            return await PostAsync<object>(urlBuilder.ToString(), new object());
        }
    }
}
