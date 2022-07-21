using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals
{
    public class ReferralQuery
    {
        [JsonPropertyName("primaryInfo")]
        public Primaryinfo PrimaryInfo { get; set; }
    }
}
