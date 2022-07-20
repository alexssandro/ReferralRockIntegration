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

        public async Task AddAsync()
        {
            await _referralRepository.AddAsync();
        }

        public async Task EditAsync()
        {
            await _referralRepository.EditAsync();
        }

        public async Task RemoveAsync(string id)
        {
            await _referralRepository.RemoveAsync(id);
        }
    }
}