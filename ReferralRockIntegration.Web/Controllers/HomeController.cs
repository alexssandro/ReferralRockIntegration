using Microsoft.AspNetCore.Mvc;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models;
using ReferralRockIntegration.Web.Models;
using System.Diagnostics;

namespace ReferralRockIntegration.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReferralRockApiWrapper _referralRockApiWrapper;

        public HomeController(IReferralRockApiWrapper referralRockApiWrapper)
        {
            _referralRockApiWrapper = referralRockApiWrapper;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            //var members = await _referralRockApiWrapper.GetAllMembersAsync<List<MemberRequestParameter>>(new MemberRequestParameter());
            //return View(members);
            return View();
        }
    }
}