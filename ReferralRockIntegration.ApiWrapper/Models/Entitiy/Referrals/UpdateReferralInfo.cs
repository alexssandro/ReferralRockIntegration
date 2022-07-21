using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals
{
    public class UpdateReferralInfo
    {
        [JsonPropertyName("query")]
        public ReferralQuery Query { get; set; }
        [JsonPropertyName("referral")]
        public ReferralRegister Referral { get; set; }
    }
}
