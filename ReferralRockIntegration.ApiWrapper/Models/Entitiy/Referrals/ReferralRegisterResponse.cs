using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Referrals
{
    public class ReferralRegisterResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("referral")]
        public Referral Referral { get; set; }
    }
}