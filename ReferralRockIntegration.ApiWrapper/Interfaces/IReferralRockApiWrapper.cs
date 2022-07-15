using ReferralRockIntegration.ApiWrapper.Models;
using System.Collections;

namespace ReferralRockIntegration.ApiWrapper.Interfaces
{
    public interface IReferralRockApiWrapper
    {
        Task<T> GetAllMembersAsync<T>(MemberRequestParameter memberRequest) where T : IList;
    }
}
