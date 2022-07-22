using ReferralRockIntegration.ApiWrapper.Models.Referrals;
using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals
{
    public class UpdateReferralInfoResponse
    {
        [JsonPropertyName("query")]
        public ReferralQuery Query { get; set; }
        [JsonPropertyName("referral")]
        public Referral Referral { get; set; }
        [JsonPropertyName("resultInfo")]
        public UpdateReferralResultInfo ResultInfo { get; set; }
    }

    public class UpdateReferralResultInfo
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

}
