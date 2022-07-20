using Microsoft.AspNetCore.Mvc;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Referral;
using ReferralRockIntegration.Web.Models;

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

            if (referrals == null)
                return NotFound();

            return View(referrals);
        }

        [HttpGet("create/{memberId:guid}")]
        public IActionResult Create(string memberId)
        {
            var referral = new ReferralViewModel
            {
                MemberId = memberId
            };
            return View(referral);
        }

        [HttpPost("create")]
        public IActionResult Create(ReferralViewModel referralViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return View(referralViewModel);
        }


        [HttpGet("edit/{memberId:guid}/{id:guid}")]
        public IActionResult Edit(string memberId, string id)
        {
            var referral = new ReferralViewModel
            {
                MemberId = memberId
            };
            return View(referral);
        }

        [HttpPost("edit")]
        public IActionResult Edit(ReferralViewModel referralViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return View(referralViewModel);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(string id)
        {


            return NoContent();
        }
    }
}
