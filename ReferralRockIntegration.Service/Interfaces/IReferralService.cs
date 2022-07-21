using ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referral;

namespace ReferralRockIntegration.Service.Interfaces
{
    public interface IReferralService
    {
        Task AddAsync(ReferralRegister referralRegister);
        Task EditAsync();
        Task RemoveAsync(string id);
    }
}
