using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals
{
    public class ReferralRemoveInfo
    {
        [JsonPropertyName("query")]
        public ReferralQuery Query { get; set; }
    }
}
