using ReferralRockIntegration.ApiWrapper.Models.Member;

namespace ReferralRockIntegration.ApiWrapper.Models.Referral
{
    public class ReferralRequestParameter : RequestParameterBase
    {
        public string Status { get; set; }
        public string MemberId { get; set; }
    }
}
