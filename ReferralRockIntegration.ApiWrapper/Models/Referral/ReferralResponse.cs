using ReferralRockIntegration.ApiWrapper.Models.HttpData;
using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Referral
{
    public class ReferralResponse : GeneralGetResponse
    {
        [JsonPropertyName("referrals")]
        public Referral[] Referrals { get; set; }
    }

    public class Referral : ReferralRockEntity
    {
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonPropertyName("amount")]
        public float Amount { get; set; }
        [JsonPropertyName("amountFormatted")]
        public string AmountFormatted { get; set; }
        [JsonPropertyName("preferredContact")]
        public string PreferredContact { get; set; }
        [JsonPropertyName("createDate")]
        public DateTime CreateDate { get; set; }
        [JsonPropertyName("updateDate")]
        public DateTime UpdateDate { get; set; }
        [JsonPropertyName("source")]
        public string Source { get; set; }
        [JsonPropertyName("referringMemberId")]
        public string ReferringMemberId { get; set; }
        [JsonPropertyName("referringMemberName")]
        public string ReferringMemberName { get; set; }
        [JsonPropertyName("memberEmail")]
        public string MemberEmail { get; set; }
        [JsonPropertyName("memberReferralCode")]
        public string MemberReferralCode { get; set; }
        [JsonPropertyName("memberExternalIdentifier")]
        public string MemberExternalIdentifier { get; set; }
        [JsonPropertyName("approvedDate")]
        public object ApprovedDate { get; set; }
        [JsonPropertyName("qualifiedDate")]
        public DateTime QualifiedDate { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("companyName")]
        public string CompanyName { get; set; }
        [JsonPropertyName("note")]
        public string Note { get; set; }
        [JsonPropertyName("publicNote")]
        public string PublicNote { get; set; }
        [JsonPropertyName("customOption2Name")]
        public string CustomOption2Name { get; set; }
        [JsonPropertyName("customText3Name")]
        public string CustomText3Name { get; set; }
        [JsonPropertyName("customOption2Value")]
        public string CustomOption2Value { get; set; }
        [JsonPropertyName("customText3Value")]
        public string CustomText3Value { get; set; }
        [JsonPropertyName("conversionNote")]
        public string ConversionNote { get; set; }
        [JsonPropertyName("conversionIPAddress")]
        public string ConversionIPAddress { get; set; }
        [JsonPropertyName("IPAddressSource")]
        public string IPAddressSource { get; set; }
    }
}