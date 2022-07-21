using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals
{
    public class Primaryinfo
    {
        [JsonPropertyName("referralId")]
        public string ReferralId { get; set; }
    }
}
