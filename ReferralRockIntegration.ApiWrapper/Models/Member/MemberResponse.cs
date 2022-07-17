using ReferralRockIntegration.ApiWrapper.Models.HttpData;
using System.Text.Json.Serialization;

namespace ReferralRockIntegration.ApiWrapper.Models.Member
{
    public class MemberResponse : GeneralGetResponse
    {
        [JsonPropertyName("members")]
        public Member[] Members { get; set; }
    }

    public class Member : ReferralRockEntity
    {
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
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
        [JsonPropertyName("lastViewIPAddress")]
        public string LastViewIPAddress { get; set; }
    }
}