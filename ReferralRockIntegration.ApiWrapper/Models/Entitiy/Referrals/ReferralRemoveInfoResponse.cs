using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals
{
    public class ReferralRemoveInfoResponse
    {
        [JsonPropertyName("query")]
        public ReferralQuery Query { get; set; }
        [JsonPropertyName("resultInfo")]
        public UpdateReferralResultInfo ResultInfo { get; set; }
    }
}
