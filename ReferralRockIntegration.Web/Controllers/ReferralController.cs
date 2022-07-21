using Microsoft.AspNetCore.Mvc;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Referral;
using ReferralRockIntegration.Service.Interfaces;
using ReferralRockIntegration.Web.Models;

namespace ReferralRockIntegration.Web.Controllers
{
    [Route("ref")]
    public class ReferralController : MainController
    {
        private readonly IReferralRepository _referralRepository;
        private readonly IMemberRepository _memberRepository;

        public ReferralController(INotifier notifier,
                                  IReferralRepository referralRepository,
                                  IMemberRepository memberRepository)
            : base(notifier)
        {
            _referralRepository = referralRepository;
            _memberRepository = memberRepository;
        }

        [Route("")]
        public async Task<IActionResult> Index(ReferralRequestParameter requestParameter)
        {
            if (string.IsNullOrEmpty(requestParameter.MemberId))
                return NotFound();

            var member = await _memberRepository.GetByIdAsync(requestParameter.MemberId);

            if (member == null)
                return NotFound();

            var referrals = await _referralRepository.SearchAsync(requestParameter);

            ViewBag.PageTitle = $"{member.FirstName} Referrals";
            ViewBag.MemberId = requestParameter.MemberId;
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
        public async Task<IActionResult> Create(string memberId)
        {
            if (string.IsNullOrEmpty(memberId))
                return NotFound();

            var member = await _memberRepository.GetByIdAsync(memberId);

            if (member == null)
                return NotFound();

            ViewBag.PageTitle = $"Create a new {member.FirstName}'s referral";

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
                return CustomResponse(ModelState);

            return Redirect($"/ref/actionresult/{referralViewModel.MemberId}/{referralViewModel.Id}");
        }

        //[HttpGet("actionresult/{memberId:guid}/{referralId:guid}")]
        [HttpGet("actionresult/{memberId}/{referralId}")]
        public async Task<IActionResult> ShowResult(string memberId, string referralId)
        {
            //if (string.IsNullOrEmpty(memberId) || string.IsNullOrEmpty(referralId))
            //    return NotFound();
            //
            //var member = await _memberRepository.GetByIdAsync(memberId);
            //
            //if (member == null)
            //    return NotFound();
            //
            //var referral = await _referralRepository.GetByIdAsync(referralId);
            //
            //if (referral == null)
            //    return NotFound();
            //
            //var referralResult = new ReferralResultViewModel
            //{
            //    ReferralName = referral.FullName,
            //    MemberName = $"{member.FirstName} {member.LastName}"
            //};

            var referralResult = new ReferralResultViewModel
            {
                MemberId = memberId,
                ReferralName = "John Referral",
                MemberName = "Steve Member"
            };

            return View(referralResult);
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
