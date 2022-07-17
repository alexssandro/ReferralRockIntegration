using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.HttpData
{
    public class GeneralGetResponse
    {
        [JsonPropertyName("offset")]
        public int Offset { get; set; }
        [JsonPropertyName("total")]
        public int Total { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
