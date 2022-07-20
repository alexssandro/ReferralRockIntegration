using ReferralRockIntegration.ApiWrapper.Models.Referral;

namespace ReferralRockIntegration.ApiWrapper.Interfaces
{
    public interface IReferralRepository
    {
        Task<object> Add();
        Task<object> Edit();
        Task<object> Remove();
        Task<ReferralResponse> SearchAsync(ReferralRequestParameter memberRequest);
    }
}