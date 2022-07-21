using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referral
{
    public class ReferralRegister
    {
        public ReferralRegister()
        {
            Id = Guid.NewGuid().ToString();
        }

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
