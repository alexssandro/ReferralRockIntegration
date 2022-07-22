using ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals;
using ReferralRockIntegration.ApiWrapper.Models.Referrals;

namespace ReferralRockIntegration.ApiWrapper.Interfaces
{
    public interface IReferralRepository
    {
        Task<ReferralRegisterResponse> AddAsync(ReferralRegister referralRegister);
        Task<UpdateReferralInfoResponse> EditAsync(UpdateReferralInfo[] referralRegister);
        Task<Referral> GetByCodeAsync(string id);
        Task<ReferralRemoveInfoResponse> RemoveAsync(ReferralRemoveInfo[] referralRemoveInfo);
        Task<ReferralResponse> SearchAsync(ReferralRequestParameter memberRequest);
    }
}