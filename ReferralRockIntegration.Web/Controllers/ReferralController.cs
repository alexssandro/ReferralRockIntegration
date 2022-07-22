using Microsoft.AspNetCore.Mvc;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals;
using ReferralRockIntegration.ApiWrapper.Models.Referrals;
using ReferralRockIntegration.Service.Interfaces;
using ReferralRockIntegration.Web.Models;
using ReferralRockIntegration.Web.Models.Enum;

namespace ReferralRockIntegration.Web.Controllers
{
    [Route("ref")]
    public class ReferralController : MainController
    {
        private readonly IReferralRepository _referralRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IReferralService _referralService;

        public ReferralController(INotifier notifier,
                                  IReferralRepository referralRepository,
                                  IMemberRepository memberRepository,
                                  IReferralService referralService)
            : base(notifier)
        {
            _referralRepository = referralRepository;
            _memberRepository = memberRepository;
            _referralService = referralService;
        }

        [Route("")]
        public async Task<IActionResult> Index(ReferralRequestParameter requestParameter)
        {
            if (string.IsNullOrEmpty(requestParameter.MemberId))
                return NotFound();

            bool validGuid = Guid.TryParse(requestParameter.MemberId, out _);

            if (!validGuid)
                return NotFound();

            var member = await _memberRepository.GetByCodeAsync(requestParameter.MemberId);

            if (member == null)
                return NotFound();

            var referrals = await _referralRepository.SearchAsync(requestParameter);

            ViewBag.PageTitle = $"{member.FirstName} Referrals";

            var referralsViewModel = new ReferralsViewModel
            {
                MemberId = member.Id,
                MemberName = member.FirstName,
                ReferringCode = member.ReferralCode,
                ReferralResponse = referrals
            };

            return View(referralsViewModel);
        }

        [Route("{code:guid}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var referral = await _referralRepository.GetByCodeAsync(code);

            if (referral == null)
                return NotFound();

            return Ok(referral);
        }

        [HttpGet("create/{referralCode}")]
        public async Task<IActionResult> Create(string referralCode)
        {
            if (string.IsNullOrEmpty(referralCode))
                return NotFound();

            var member = await _memberRepository.GetByCodeAsync(referralCode);

            if (member == null)
                return NotFound();

            ViewBag.PageTitle = $"Create a new {member.FirstName}'s referral";

            var referral = new ReferralViewModel
            {
                MemberId = member.Id,
                ReferralCode = referralCode,
                FormAction = FormAction.Create
            };

            return View(referral);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ReferralViewModel referralViewModel)
        {
            if (!ModelState.IsValid)
                return View(referralViewModel);

            var referralRegister = new ReferralRegister
            {
                Email = referralViewModel.Email,
                FirstName = referralViewModel.FirstName,
                LastName = referralViewModel.LastName,
                ReferralCode = referralViewModel.ReferralCode
            };

            var response = await _referralService.AddAsync(referralRegister);

            if (IsThereAnyError())
                return View(referralViewModel);

            return Redirect($"/ref/actionresult/{response.Referral.MemberReferralCode}/{response.Referral.Id}");
        }

        [HttpGet("actionresult/{referralCode}/{referralId:guid}")]
        public async Task<IActionResult> ShowResult(string referralCode, string referralId)
        {
            if (string.IsNullOrEmpty(referralCode) || string.IsNullOrEmpty(referralId))
                return NotFound();

            var member = await _memberRepository.GetByCodeAsync(referralCode);

            if (member == null)
                return NotFound();

            var referral = await _referralRepository.GetByCodeAsync(referralId);

            if (referral == null)
                return NotFound();

            var referralResult = new ReferralResultViewModel
            {
                MemberId = member.Id,
                ReferralCode = referralCode,
                ReferralName = referral.FullName,
                MemberName = $"{member.FirstName} {member.LastName}"
            };

            return View(referralResult);
        }


        [HttpGet("edit/{referralCode}/{id:guid}")]
        public async Task<IActionResult> Edit(string referralCode, string id)
        {
            if (string.IsNullOrEmpty(referralCode) || string.IsNullOrEmpty(id))
                return NotFound();

            var member = await _memberRepository.GetByCodeAsync(referralCode);

            if (member == null)
                return NotFound();

            var referral = await _referralRepository.GetByCodeAsync(id);

            if (referral == null)
                return NotFound();

            ViewBag.PageTitle = $"Create a new {member.FirstName}'s referral";

            var referralViewModel = new ReferralViewModel
            {
                Id = id,
                ReferralCode = referralCode,
                MemberId = member.Id,
                Email = referral.Email,
                FirstName = referral.FirstName,
                LastName = referral.LastName,
                FormAction = FormAction.Update
            };
            return View(referralViewModel);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(ReferralViewModel referralViewModel)
        {
            if (referralViewModel == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(referralViewModel);

            var referralRegister = new ReferralRegister
            {
                Id = referralViewModel.Id,
                Email = referralViewModel.Email,
                FirstName = referralViewModel.FirstName,
                LastName = referralViewModel.LastName,
                ReferralCode = referralViewModel.ReferralCode
            };

            var response = await _referralService.EditAsync(referralRegister);

            if (IsThereAnyError())
                return View(referralViewModel);

            return Redirect($"/ref/actionresult/{response.Referral.MemberReferralCode}/{response.Referral.Id}");
        }

        [HttpGet("delete/{referralId:guid}")]
        public async Task<IActionResult> ConfirmDelete(string referralId)
        {
            var referral = await _referralRepository.GetByCodeAsync(referralId);

            if (referral == null)
                return NotFound();

            return View(referral);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _referralService.RemoveAsync(id);

            if (HasNotification())
                return BadRequest(GetNotifications());

            return NoContent();
        }
    }
}
