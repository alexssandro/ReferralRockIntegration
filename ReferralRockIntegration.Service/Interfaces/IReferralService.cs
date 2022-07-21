using ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referral;
using ReferralRockIntegration.ApiWrapper.Models.Referral;

namespace ReferralRockIntegration.Service.Interfaces
{
    public interface IReferralService
    {
        Task<ReferralRegisterResponse> AddAsync(ReferralRegister referralRegister);
        Task EditAsync();
        Task RemoveAsync(string id);
    }
}
