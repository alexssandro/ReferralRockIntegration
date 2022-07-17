using ReferralRockIntegration.ApiWrapper.Models.Member;

namespace ReferralRockIntegration.ApiWrapper.Interfaces
{
    public interface IMemberRepository
    {
        Task<MemberResponse> SearchAsync(MemberRequestParameter memberRequest);
    }
}
