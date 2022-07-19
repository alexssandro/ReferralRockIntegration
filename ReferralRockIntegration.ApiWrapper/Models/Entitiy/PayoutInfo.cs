using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Entitiy
{
    public class Payoutinfo
    {
        [JsonPropertyName("payoutType")]
        public string PayoutType { get; set; }
        [JsonPropertyName("useDefaultValues")]
        public bool UseDefaultValues { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
