using ReferralRockIntegration.ApiWrapper.Models.HttpData;

namespace ReferralRockIntegration.ApiWrapper.Models.Referrals
{
    public class ReferralRequestParameter : RequestParameterBase
    {
        public string Status { get; set; }
        public string MemberId { get; set; }
    }
}
