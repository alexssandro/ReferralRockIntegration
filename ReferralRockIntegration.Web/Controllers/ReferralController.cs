using Microsoft.AspNetCore.Mvc;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Referral;
using ReferralRockIntegration.Web.Models;

namespace ReferralRockIntegration.Web.Controllers
{
    [Route("ref")]
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

        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(string id)
        {
            var referral = await _referralRepository.GetByIdAsync(id);

            if (referral == null)
                return NotFound();

            return Ok(referral);
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
