using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models
{
    public abstract class ReferralRockEntity
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("externalIdentifier")]
        public string ExternalIdentifier { get; set; }
        [JsonPropertyName("programId")]
        public string ProgramId { get; set; }
        [JsonPropertyName("programTitle")]
        public string ProgramTitle { get; set; }
        [JsonPropertyName("programName")]
        public string ProgramName { get; set; }
        [JsonPropertyName("customOption1Name")]
        public string CustomOption1Name { get; set; }
        [JsonPropertyName("customText1Name")]
        public string CustomText1Name { get; set; }
        [JsonPropertyName("customText2Name")]
        public string CustomText2Name { get; set; }
        [JsonPropertyName("customOption1Value")]
        public string CustomOption1Value { get; set; }
        [JsonPropertyName("customText1Value")]
        public string CustomText1Value { get; set; }
        [JsonPropertyName("customText2Value")]
        public string CustomText2Value { get; set; }
        [JsonPropertyName("utmSource")]
        public string UtmSource { get; set; }
        [JsonPropertyName("utmMedium")]
        public string UtmMedium { get; set; }
        [JsonPropertyName("utmCampaign")]
        public string UtmCampaign { get; set; }
        [JsonPropertyName("browserReferrerUrl")]
        public string BrowserReferrerUrl { get; set; }
    }
}
