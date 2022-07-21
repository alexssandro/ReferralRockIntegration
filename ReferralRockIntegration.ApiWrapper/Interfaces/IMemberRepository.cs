using ReferralRockIntegration.ApiWrapper.Models.Member;

namespace ReferralRockIntegration.ApiWrapper.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member> GetByCodeAsync(string id);
        Task<MemberResponse> SearchAsync(MemberRequestParameter memberRequest);
    }
}
