using ReferralRockIntegration.ApiWrapper.Models;

namespace ReferralRockIntegration.ApiWrapper.Interfaces
{
    public interface IReferralRockApiWrapper
    {
        Task<MemberResponse> GetAllMembersAsync(MemberRequestParameter memberRequest);
    }
}
