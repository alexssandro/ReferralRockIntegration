using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.Service.Interfaces;

namespace ReferralRockIntegration.Service
{
    public class ReferralService : IReferralService
    {
        private readonly IReferralRepository _referralRepository;

        public ReferralService(IReferralRepository referralRepository)
        {
            _referralRepository = referralRepository;
        }

        public async Task Add()
        {
            await _referralRepository.Add();
        }

        public async Task Edit()
        {
            await _referralRepository.Edit();
        }

        public async Task Remove()
        {
            await _referralRepository.Remove();
        }
    }
}