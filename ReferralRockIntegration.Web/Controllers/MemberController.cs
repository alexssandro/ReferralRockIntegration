using Microsoft.AspNetCore.Mvc;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Member;
using ReferralRockIntegration.Service.Interfaces;

namespace ReferralRockIntegration.Web.Controllers
{
    [Route("")]
    public class MemberController : MainController
    {
        private readonly IMemberRepository _memberRepository;

        public MemberController(INotifier notifier, IMemberRepository referralRockApiWrapper)
            :base(notifier)
        {
            _memberRepository = referralRockApiWrapper;
        }

        [Route("")]
        public async Task<IActionResult> Index(MemberRequestParameter requestParameters)
        {
            ViewBag.PageTitle = "Members";
            var members = await _memberRepository.SearchAsync(requestParameters);
            return View(members);
        }
    }
}