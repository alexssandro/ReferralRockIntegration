using ReferralRockIntegration.ApiWrapper.Models.HttpData;
using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Referrals
{
    public class ReferralResponse : GeneralGetResponse
    {
        [JsonPropertyName("referrals")]
        public Referral[] Referrals { get; set; }
    }
}