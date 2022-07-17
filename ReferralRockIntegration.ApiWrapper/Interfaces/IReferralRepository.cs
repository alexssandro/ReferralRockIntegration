using ReferralRockIntegration.ApiWrapper.Models.Referral;

namespace ReferralRockIntegration.ApiWrapper.Interfaces
{
    public interface IReferralRepository
    {
        Task<ReferralResponse> SearchAsync(ReferralRequestParameter memberRequest);
    }
}