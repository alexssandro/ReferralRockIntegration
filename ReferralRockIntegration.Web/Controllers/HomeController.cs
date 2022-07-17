using Microsoft.AspNetCore.Mvc;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Member;

namespace ReferralRockIntegration.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemberRepository _referralRockApiWrapper;

        public HomeController(IMemberRepository referralRockApiWrapper)
        {
            _referralRockApiWrapper = referralRockApiWrapper;
        }

        [Route("")]
        public async Task<IActionResult> Index(MemberRequestParameter requestParameters)
        {
            var members = await _referralRockApiWrapper.SearchAsync(requestParameters);
            return View(members);
        }
    }
}