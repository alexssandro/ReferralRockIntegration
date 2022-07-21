using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals;
using ReferralRockIntegration.ApiWrapper.Models.Referrals;
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
                Notify("Referral Code must be valid", "ReferralCode");

            var referralEmail = await _referralRepository.GetByCodeAsync(referralRegister.Email);

            if (referralEmail != null)
                Notify("Another referral with this e-mail already exists", "Email");

            if (_notifier.HasNotification())
                return null;

            var response = await _referralRepository.AddAsync(referralRegister);

            if (response.Message != "Referral Added")
                Notify($"Failed to register, reason: {response.Message}");

            return response;
        }

        public async Task<UpdateReferralInfoResponse> EditAsync(ReferralRegister referralRegister)
        {
            var member = await _memberRepository.GetByCodeAsync(referralRegister.ReferralCode);

            if (member == null)
                Notify("Referral Code must be valid", "ReferralCode");

            var referral = await _referralRepository.GetByCodeAsync(referralRegister.Id);

            if (referral == null)
                Notify("The referral informed does not exists");

            var referralEmail = await _referralRepository.GetByCodeAsync(referralRegister.Email);

            if (referralEmail != null && referralEmail.Id != referralRegister.Id)
                Notify("Another referral with this e-mail already exists", "Email");

            if (_notifier.HasNotification())
                return null;

            var updateReferralInfo = new UpdateReferralInfo
            {
                Referral = referralRegister,
                Query = new ReferralQuery
                {
                    PrimaryInfo = new Primaryinfo
                    {
                        ReferralId = referralRegister.Id
                    }
                }
            };

            var response = await _referralRepository.EditAsync(new UpdateReferralInfo[] { updateReferralInfo });

            if (response.resultInfo.Status != "Succeeded")
                Notify(response.resultInfo.Message);

            return response;
        }

        public async Task RemoveAsync(string id)
        {
            await _referralRepository.RemoveAsync(id);
        }
    }
}