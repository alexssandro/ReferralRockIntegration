using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals;
using ReferralRockIntegration.ApiWrapper.Models.Member;
using ReferralRockIntegration.ApiWrapper.Models.Referrals;
using ReferralRockIntegration.Service.Interfaces;
using ReferralRockIntegration.Service.Models.Notification;
using ReferralRockIntegration.Web.Controllers;
using ReferralRockIntegration.Web.Models;

namespace ReferralRockIntegration.Web.Tests
{
    public class ReferralControllerTests
    {
        private readonly ReferralController _referralController;
        private readonly Mock<INotifier> _notifier;
        private readonly Mock<IReferralRepository> _referralRepository;
        private readonly Mock<IMemberRepository> _memberRepository;
        private readonly Mock<IReferralService> _referralService;

        public ReferralControllerTests()
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

        [Fact]
        public async Task Post_Create_GivenReferralViewModel_WhenThereIsNotificationInSeviceLayer_ShouldReturnViewResultWithData()
        {
            var referralViewModel = new ReferralViewModel
            {
                FirstName = "First",
                LastName = "One",
                Email = "firstone@gmail.com",
            };

            _notifier.Setup(_ => _.GetNotifications())
                     .Returns(new List<Notification>
                     {
                         new Notification( null, "someMessage")
                     });

            _notifier.Setup(_ => _.HasNotification())
                     .Returns(true);

            var result = await _referralController.Create(referralViewModel);

            _referralService.Verify(_ => _.AddAsync(It.Is<ReferralRegister>(__ =>
                    __.FirstName == "First"
                 && __.LastName == "One"
                 && __.Email == "firstone@gmail.com")), Times.Once);

            result.Should().BeOfType<ViewResult>();
            var viewResult = (ViewResult)result;
            viewResult.Model.Should().BeOfType<ReferralViewModel>();

        }

        [Fact]
        public async Task Post_Create_GivenReferralViewModel_WhenThereIsNotNotificationInSeviceLayer_ShouldReturnRedirect()
        {
            var referralViewModel = new ReferralViewModel
            {
                FirstName = "First",
                LastName = "One",
                Email = "firstone@gmail.com",
            };

            _notifier.Setup(_ => _.GetNotifications())
                     .Returns(new List<Notification>());

            _notifier.Setup(_ => _.HasNotification())
                     .Returns(false);

            _referralService.Setup(_ => _.AddAsync(It.IsAny<ReferralRegister>()))
                            .ReturnsAsync(new ReferralRegisterResponse
                            {
                                Referral = new Referral
                                {
                                    MemberReferralCode = "1010",
                                    Id = "123"
                                }
                            });

            var result = await _referralController.Create(referralViewModel);

            _referralService.Verify(_ => _.AddAsync(It.Is<ReferralRegister>(__ =>
                    __.FirstName == "First"
                 && __.LastName == "One"
                 && __.Email == "firstone@gmail.com")), Times.Once);

            result.Should().BeOfType<RedirectResult>();
            var redirectResult = (RedirectResult)result;
            redirectResult.Url.Should().Be("/ref/actionresult/1010/123");
        }

        [Fact]
        public async Task Post_Create_GivenReferralViewModel_WhenModelStateIsNotValid_ShouldReturnViewWithErrors()
        {
            var referralViewModel = new ReferralViewModel
            {
                FirstName = null,
                LastName = null,
                Email = "firstone@gmail.com",
            };

            _referralController.ModelState.AddValidationErrors(referralViewModel);

            var result = await _referralController.Create(referralViewModel);

            _referralService.Verify(_ => _.AddAsync(It.Is<ReferralRegister>(__ =>
                    __.FirstName == "First"
                 && __.LastName == "One"
                 && __.Email == "firstone@gmail.com")), Times.Never);
            
            result.Should().BeOfType<ViewResult>();
            var viewResult = (ViewResult)result;
            var resultReferralViewModel = (ReferralViewModel)viewResult.Model;
            resultReferralViewModel.FirstName.Should().Be(null);
            resultReferralViewModel.LastName.Should().Be(null);
            resultReferralViewModel.Email.Should().Be("firstone@gmail.com");

            _referralController.ModelState.ErrorCount.Should().Be(2);

            _referralController.ModelState["FirstName"].Errors.Count.Should().Be(1);
            _referralController.ModelState["LastName"].Errors.Count.Should().Be(1);
            _referralController.ModelState["Email"].Should().Be(null);
        }
    }
}
