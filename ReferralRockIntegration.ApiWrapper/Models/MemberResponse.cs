using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models
{
    public class MemberResponse
    {
        [JsonPropertyName("offset")]
        public int Offset { get; set; }
        [JsonPropertyName("total")]
        public int Total { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("members")]
        public Member[] Members { get; set; }
    }

    public class Member
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
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("externalIdentifier")]
        public string ExternalIdentifier { get; set; }
        [JsonPropertyName("dateOfBirth")]
        public object DateOfBirth { get; set; }
        [JsonPropertyName("addressLine1")]
        public string AddressLine1 { get; set; }
        [JsonPropertyName("addressLine2")]
        public string AddressLine2 { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("countrySubdivision")]
        public string CountrySubdivision { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }
        [JsonPropertyName("disabledFlag")]
        public bool DisabledFlag { get; set; }
        [JsonPropertyName("customOverrideURL")]
        public string CustomOverrideURL { get; set; }
        [JsonPropertyName("payoutInfo")]
        public Payoutinfo PayoutInfo { get; set; }
        [JsonPropertyName("customOption1Name")]
        public string CustomOption1Name { get; set; }
        [JsonPropertyName("customOption1Value")]
        public string CustomOption1Value { get; set; }
        [JsonPropertyName("customText1Name")]
        public string CustomText1Name { get; set; }
        [JsonPropertyName("customText1Value")]
        public string CustomText1Value { get; set; }
        [JsonPropertyName("customText2Name")]
        public string CustomText2Name { get; set; }
        [JsonPropertyName("customText2Value")]
        public string CustomText2Value { get; set; }
        [JsonPropertyName("programId")]
        public string ProgramId { get; set; }
        [JsonPropertyName("programTitle")]
        public string ProgramTitle { get; set; }
        [JsonPropertyName("programName")]
        public string ProgramName { get; set; }
        [JsonPropertyName("referralUrl")]
        public string ReferralUrl { get; set; }
        [JsonPropertyName("referralCode")]
        public string ReferralCode { get; set; }
        [JsonPropertyName("memberUrl")]
        public string MemberUrl { get; set; }
        [JsonPropertyName("emailShares")]
        public int EmailShares { get; set; }
        [JsonPropertyName("socialShares")]
        public int SocialShares { get; set; }
        [JsonPropertyName("views")]
        public int Views { get; set; }
        [JsonPropertyName("referrals")]
        public int Referrals { get; set; }
        [JsonPropertyName("lastShare")]
        public object LastShare { get; set; }
        [JsonPropertyName("referralsApproved")]
        public int ReferralsApproved { get; set; }
        [JsonPropertyName("referralsQualified")]
        public int ReferralsQualified { get; set; }
        [JsonPropertyName("referralsPending")]
        public int ReferralsPending { get; set; }
        [JsonPropertyName("referralsApprovedAmount")]
        public float ReferralsApprovedAmount { get; set; }
        [JsonPropertyName("rewardsPendingAmount")]
        public float RewardsPendingAmount { get; set; }
        [JsonPropertyName("rewardsIssuedAmount")]
        public float RewardsIssuedAmount { get; set; }
        [JsonPropertyName("rewardAmountTotal")]
        public float RewardAmountTotal { get; set; }
        [JsonPropertyName("rewards")]
        public int Rewards { get; set; }
        [JsonPropertyName("createDt")]
        public DateTime CreateDate { get; set; }
        [JsonPropertyName("utmSource")]
        public string UtmSource { get; set; }
        [JsonPropertyName("utmMedium")]
        public string UtmMedium { get; set; }
        [JsonPropertyName("utmCampaign")]
        public string UtmCampaign { get; set; }
        [JsonPropertyName("browserReferrerUrl")]
        public string BrowserReferrerUrl { get; set; }
        [JsonPropertyName("lastViewIPAddress")]
        public string LastViewIPAddress { get; set; }
    }

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
