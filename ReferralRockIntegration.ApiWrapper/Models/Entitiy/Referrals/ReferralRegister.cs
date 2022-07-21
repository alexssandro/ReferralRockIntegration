using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals
{
    public class ReferralRegister
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("referralCode")]
        public string ReferralCode { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
