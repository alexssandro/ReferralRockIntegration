using Microsoft.AspNetCore.Mvc;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Referral;

namespace ReferralRockIntegration.Web.Controllers
{
    public class ReferralController : Controller
    {
        private readonly IReferralRepository _referralRepository;

        public ReferralController(IReferralRepository referralRepository)
        {
            _referralRepository = referralRepository;
        }

        public async Task<IActionResult> Index(ReferralRequestParameter requestParameter)
        {
            if (string.IsNullOrEmpty(requestParameter.MemberId))
                return NotFound();

            var referrals = await _referralRepository.SearchAsync(requestParameter);

            if (referrals == null || !referrals.Referrals.Any())
                return NotFound();

            return View(referrals);
        }
    }
}
