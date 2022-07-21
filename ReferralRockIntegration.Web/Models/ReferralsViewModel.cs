using ReferralRockIntegration.ApiWrapper.Models.Referrals;

namespace ReferralRockIntegration.Web.Models
{
    public class ReferralsViewModel
    {
        public string MemberName { get; set; }
        public string ReferringCode { get; set; }
        public string MemberId { get; set; }
        public ReferralResponse ReferralResponse { get; set; }
    }
}
