using ReferralRockIntegration.ApiWrapper.Models.Referral;

namespace ReferralRockIntegration.ApiWrapper.Interfaces
{
    public interface IReferralRepository
    {
        Task<object> AddAsync();
        Task<object> EditAsync();
        Task<Referral> GetByIdAsync(string id);
        Task<object> RemoveAsync(string id);
        Task<ReferralResponse> SearchAsync(ReferralRequestParameter memberRequest);
    }
}