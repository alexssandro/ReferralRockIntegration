using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Member;
using ReferralRockIntegration.ApiWrapper.Models.Referrals;
using ReferralRockIntegration.Service.Interfaces;
using ReferralRockIntegration.Web.Controllers;
using ReferralRockIntegration.ApiWrapper.Models.Member;
using ReferralRockIntegration.Web.Models;

namespace ReferralRockIntegration.Web.Tests
{
    public class ReferralControllerTest
    {
        private readonly ReferralController _referralController;
        private readonly Mock<INotifier> _notifier;
        private readonly Mock<IReferralRepository> _referralRepository;
        private readonly Mock<IMemberRepository> _memberRepository;
        private readonly Mock<IReferralService> _referralService;

        public ReferralControllerTest()
        {
            _notifier = new Mock<INotifier>();
            _referralRepository = new Mock<IReferralRepository>();
            _memberRepository = new Mock<IMemberRepository>();
            _referralService = new Mock<IReferralService>();
            _referralController = new ReferralController(
                    _notifier.Object,
                    _referralRepository.Object,
                    _memberRepository.Object,
                    _referralService.Object
                    );
        }

        [Fact]
        public async Task Index_GivenNoMemberId_ShoulReturnNotfound()
        {
            var parameter = new ReferralRequestParameter
            {
                MemberId = string.Empty
            };

            var result = await _referralController.Index(parameter);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Index_GivenMemberIdInvalidGuid_ShoulReturnNotfound()
        {
            var parameter = new ReferralRequestParameter
            {
                MemberId = "1"
            };

            var result = await _referralController.Index(parameter);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Index_GivenMemberThatDoesNotExists_ShoulReturnNotfound()
        {
            string memberId = Guid.NewGuid().ToString();

            var parameter = new ReferralRequestParameter
            {
                MemberId = memberId
            };

            _memberRepository.Setup(_ => _.GetByCodeAsync(memberId))
                             .ReturnsAsync((Member)null);

            var result = await _referralController.Index(parameter);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Index_GivenMemberThatExists_ShoulReturnUnderlineList()
        {
            string memberId = Guid.NewGuid().ToString();

            var parameter = new ReferralRequestParameter
            {
                MemberId = memberId
            };

            _memberRepository.Setup(_ => _.GetByCodeAsync(memberId))
                             .ReturnsAsync(new Member { Id = "1010", FirstName = "test first name", ReferralCode = "testfirstname1010" });

            _referralRepository.Setup(_ => _.SearchAsync(It.Is<ReferralRequestParameter>(__ => __.MemberId == memberId)))
                               .ReturnsAsync(new ReferralResponse
                               {
                                   Referrals = new Referral[]
                                   {
                                       new Referral { Id = "1", FirstName = "referral 1" },
                                       new Referral { Id = "2", FirstName = "referral 2" },
                                       new Referral { Id = "3", FirstName = "referral 3" },
                                   }
                               });

            var result = await _referralController.Index(parameter);

            result.Should().BeOfType<ViewResult>();
            var viewResult = (ViewResult)result;
            viewResult.Model.Should().BeOfType<ReferralsViewModel>();
            var referralViewModel = (ReferralsViewModel)viewResult.Model;
            referralViewModel.ReferralResponse.Referrals.Length.Should().Be(3);
            referralViewModel.MemberId.Should().Be("1010");
            referralViewModel.MemberName.Should().Be("test first name");
            referralViewModel.ReferringCode.Should().Be("testfirstname1010");
            viewResult.ViewData["PageTitle"].Should().Be("test first name Referrals");
        }

        [Fact]
        public async Task GetByCode_GivenCodeOfAnInexistingReferral_ShouldReturnNotfound()
        {
            string code = Guid.NewGuid().ToString();

            var result = await _referralController.GetByCode(code);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetByCode_GivenCodeOfAnExistingReferral_ShouldReturnOkWithData()
        {
            string code = Guid.NewGuid().ToString();

            _referralRepository.Setup(_ => _.GetByCodeAsync(code))
                               .ReturnsAsync(new Referral 
                               {
                                   Id = code,
                                   FirstName = "First"
                               });

            var result = await _referralController.GetByCode(code);

            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)result;
            okObjectResult.Value.Should().BeOfType<Referral>();
            var referral = (Referral)okObjectResult.Value;

            referral.Id.Should().Be(code);
            referral.FirstName.Should().Be("First");
        }
        
        [Fact]
        public async Task Get_Create_GivenNoMemberId_ShouldReturnNotfound()
        {
            string referralCode = string.Empty;
            var result = await _referralController.Create(referralCode);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Get_Create_GivenAnInexistingReferralCode_ShouldReturnNotfound()
        {
            string referralCode = "code1010";
            var result = await _referralController.Create(referralCode);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Get_Create_GivenAnExistingReferralCode_ShouldReturnViewResultWithData()
        {
            string referralCode = "code1010";
            string id = Guid.NewGuid().ToString();

            _memberRepository.Setup(_ => _.GetByCodeAsync(referralCode))
                             .ReturnsAsync(new Member
                             {
                                 FirstName = "First",
                                 LastName = "One",
                                 Email = "firstone@gmail.com",
                                 ReferralCode = referralCode,
                                 Id = id 
                             });

            var result = await _referralController.Create(referralCode);
            result.Should().BeOfType<ViewResult>();
            var viewResult = (ViewResult)result;
            viewResult.Model.Should().BeOfType<ReferralViewModel>();
            viewResult.ViewData["PageTitle"].Should().Be("Create a new First's referral");
            var referralViewModel = (ReferralViewModel)viewResult.Model;
            referralViewModel.Email.Should().Be(null);
            referralViewModel.FirstName.Should().Be(null);
            referralViewModel.LastName.Should().Be(null);
            referralViewModel.ReferralCode.Should().Be(referralCode);
            referralViewModel.MemberId.Should().Be(id);
            referralViewModel.Id.Should().Be(null);
        }
    }
}
