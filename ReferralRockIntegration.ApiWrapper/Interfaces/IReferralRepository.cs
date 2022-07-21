using ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referral;
using ReferralRockIntegration.ApiWrapper.Models.Referral;

namespace ReferralRockIntegration.ApiWrapper.Interfaces
{
    public interface IReferralRepository
    {
        Task<ReferralRegisterResponse> AddAsync(ReferralRegister referralRegister);
        Task<object> EditAsync();
        Task<Referral> GetByCodeAsync(string id);
        Task<object> RemoveAsync(string id);
        Task<ReferralResponse> SearchAsync(ReferralRequestParameter memberRequest);
    }
}