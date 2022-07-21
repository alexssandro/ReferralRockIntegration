using ReferralRockIntegration.ApiWrapper.Models.HttpData;

namespace ReferralRockIntegration.ApiWrapper.Models.Member
{
    public class MemberRequestParameter : RequestParameterBase
    {
        public bool? ShowDisabled { get; set; }
    }
}
