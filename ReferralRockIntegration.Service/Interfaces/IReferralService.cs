using ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals;
using ReferralRockIntegration.ApiWrapper.Models.Referrals;

namespace ReferralRockIntegration.Service.Interfaces
{
    public interface IReferralService
    {
        Task<ReferralRegisterResponse> AddAsync(ReferralRegister referralRegister);
        Task<UpdateReferralInfoResponse> EditAsync(ReferralRegister referralRegister);
        Task RemoveAsync(string id);
    }
}
