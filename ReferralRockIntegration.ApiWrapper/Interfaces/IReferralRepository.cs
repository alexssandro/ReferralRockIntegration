using ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals;
using ReferralRockIntegration.ApiWrapper.Models.Referrals;

namespace ReferralRockIntegration.ApiWrapper.Interfaces
{
    public interface IReferralRepository
    {
        Task<ReferralRegisterResponse> AddAsync(ReferralRegister referralRegister);
        Task<UpdateReferralInfoResponse> EditAsync(UpdateReferralInfo[] referralRegister);
        Task<Referral> GetByCodeAsync(string id);
        Task<object> RemoveAsync(string id);
        Task<ReferralResponse> SearchAsync(ReferralRequestParameter memberRequest);
    }
}