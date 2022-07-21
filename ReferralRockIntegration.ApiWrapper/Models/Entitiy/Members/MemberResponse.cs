using ReferralRockIntegration.ApiWrapper.Models.HttpData;
using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Member
{
    public class MemberResponse : GeneralGetResponse
    {
        [JsonPropertyName("members")]
        public Member[] Members { get; set; }
    }
}