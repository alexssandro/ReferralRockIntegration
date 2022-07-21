using ReferralRockIntegration.ApiWrapper.Models.HttpData;
using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Referral
{
    public class ReferralResponse : GeneralGetResponse
    {
        [JsonPropertyName("referrals")]
        public Referral[] Referrals { get; set; }
    }
}