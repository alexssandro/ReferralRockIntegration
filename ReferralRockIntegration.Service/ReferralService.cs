using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referral;
using ReferralRockIntegration.ApiWrapper.Models.Referral;
using ReferralRockIntegration.Service.Interfaces;

namespace ReferralRockIntegration.Service
{
    public class ReferralService : BaseService, IReferralService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IReferralRepository _referralRepository;

        public ReferralService(INotifier notifier,
                                IReferralRepository referralRepository,
                                IMemberRepository memberRepository)
            : base(notifier)
        {
            _referralRepository = referralRepository;
            _memberRepository = memberRepository;
        }

        public async Task<ReferralRegisterResponse> AddAsync(ReferralRegister referralRegister)
        {
            var member = await _memberRepository.GetByCodeAsync(referralRegister.ReferralCode);

            if (member == null)
            {
                Notify("Referral Code must be valid", "ReferralCode");
            }

            if (_notifier.HasNotification())
                return null; ;

            var response = await _referralRepository.AddAsync(referralRegister);

            if (response.Message != "Referral Added")
                Notify($"Failed to register, reason: {response.Message}");

            return response;
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