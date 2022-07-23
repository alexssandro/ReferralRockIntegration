using FluentAssertions;
using Moq;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals;
using ReferralRockIntegration.ApiWrapper.Models.Member;
using ReferralRockIntegration.ApiWrapper.Models.Referrals;
using ReferralRockIntegration.Service.Interfaces;
using ReferralRockIntegration.Service.Models.Notification;

namespace ReferralRockIntegration.Service.Tests
{
    public class ReferralServiceTests
    {
        private readonly ReferralService _referralService;
        private readonly Mock<INotifier> _notifier;
        private readonly Mock<IReferralRepository> _referralRepository;
        private readonly Mock<IMemberRepository> _memberRepository;

        public ReferralServiceTests()
        {
            _notifier = new Mock<INotifier>();
            _referralRepository = new Mock<IReferralRepository>();
            _memberRepository = new Mock<IMemberRepository>();

            _referralService = new ReferralService(
                    _notifier.Object,
                    _referralRepository.Object,
                    _memberRepository.Object);
        }

        [Fact]
        public async Task AddAsync_GivenReferralWithAnInexistingReferralCode_ShouldReturnNull()
        {
            var referral = new ReferralRegister
            {
                ReferralCode = "1010"
            };

            _notifier.Setup(_ => _.HasNotification()).Returns(true);
            _referralRepository.Setup(_ => _.AddAsync(It.Is<ReferralRegister>(__ => __.ReferralCode == "1010")))
                               .ReturnsAsync(new ReferralRegisterResponse());

            var result = await _referralService.AddAsync(referral);
            result.Should().BeNull();
            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == "ReferralCode"
                 && __.Message == "Referral Code must be valid")), Times.Once);
        }

        [Fact]
        public async Task AddAsync_GivenReferralWithAnEmailThatHasAlreadyBeenRegistered_ShouldReturnNull()
        {
            var referral = new ReferralRegister
            {
                ReferralCode = "1010",
                Email = "firstone@gmail.com"
            };

            _memberRepository.Setup(_ => _.GetByCodeAsync("1010"))
                             .ReturnsAsync(new Member());
            _notifier.Setup(_ => _.HasNotification()).Returns(true);
            _referralRepository.Setup(_ => _.AddAsync(It.Is<ReferralRegister>(__ => __.ReferralCode == "1010")))
                               .ReturnsAsync(new ReferralRegisterResponse());
            _referralRepository.Setup(_ => _.GetByCodeAsync("firstone@gmail.com"))
                               .ReturnsAsync(new Referral());

            var result = await _referralService.AddAsync(referral);
            result.Should().BeNull();

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == "ReferralCode"
                 && __.Message == "Referral Code must be valid")), Times.Never);

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == "Email"
                 && __.Message == "Another referral with this e-mail already exists")), Times.Once);
        }

        [Fact]
        public async Task AddAsync_GivenReferral_WhenReposityReturnNotSuccessFullMessage_ShouldNotifyTheProblem()
        {
            var referral = new ReferralRegister
            {
                ReferralCode = "1010",
                Email = "firstone@gmail.com"
            };

            _memberRepository.Setup(_ => _.GetByCodeAsync("1010"))
                             .ReturnsAsync(new Member());
            _notifier.Setup(_ => _.HasNotification()).Returns(false);
            _referralRepository.Setup(_ => _.AddAsync(It.Is<ReferralRegister>(__ => __.ReferralCode == "1010")))
                               .ReturnsAsync(new ReferralRegisterResponse
                               {
                                   Message = "Referral was not added"
                               });

            var result = await _referralService.AddAsync(referral);
            result.Should().NotBeNull();

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == "ReferralCode"
                 && __.Message == "Referral Code must be valid")), Times.Never);

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == "Email"
                 && __.Message == "Another referral with this e-mail already exists")), Times.Never);

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == null
                 && __.Message == "Failed to register, reason: Referral was not added")), Times.Once);
        }

        [Fact]
        public async Task AddAsync_GivenReferral_WhenReposityReturnSuccess_ShouldNotNotifyAsItWasOK()
        {
            var referral = new ReferralRegister
            {
                ReferralCode = "1010",
                Email = "firstone@gmail.com"
            };

            _memberRepository.Setup(_ => _.GetByCodeAsync("1010"))
                             .ReturnsAsync(new Member());
            _notifier.Setup(_ => _.HasNotification()).Returns(false);
            _referralRepository.Setup(_ => _.AddAsync(It.Is<ReferralRegister>(__ => __.ReferralCode == "1010")))
                               .ReturnsAsync(new ReferralRegisterResponse
                               {
                                   Message = "Referral Added"
                               });

            var result = await _referralService.AddAsync(referral);
            result.Should().NotBeNull();

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == "ReferralCode"
                 && __.Message == "Referral Code must be valid")), Times.Never);

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == "Email"
                 && __.Message == "Another referral with this e-mail already exists")), Times.Never);

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == null
                 && __.Message == "Failed to register, reason: Referral was not added")), Times.Never);
        }

        [Fact]
        public async Task EditAsync_GivenReferralWithAnInexistingReferralCode_ShouldReturnNull()
        {
            string referralId = Guid.NewGuid().ToString();
            var referral = new ReferralRegister
            {
                ReferralCode = "1010",
                Id = referralId
            };

            _notifier.Setup(_ => _.HasNotification()).Returns(true);
            _referralRepository.Setup(_ => _.EditAsync(It.Is<UpdateReferralInfo[]>(__ => __.Any(___ => ___.Query.PrimaryInfo.ReferralId == referralId))))
                               .ReturnsAsync(new UpdateReferralInfoResponse());

            var result = await _referralService.AddAsync(referral);
            result.Should().BeNull();
            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == "ReferralCode"
                 && __.Message == "Referral Code must be valid")), Times.Once);
        }

        [Fact]
        public async Task EditAsync_GivenReferralWithAnEmailThatHasAlreadyBeenRegisteredToAnotherUser_ShouldReturnNull()
        {
            string currentReferralId = Guid.NewGuid().ToString();
            string anotherReferralId = Guid.NewGuid().ToString();
            var referral = new ReferralRegister
            {
                ReferralCode = "1010",
                Email = "firstone@gmail.com",
                Id = currentReferralId
            };

            _memberRepository.Setup(_ => _.GetByCodeAsync("1010"))
                             .ReturnsAsync(new Member());
            _notifier.Setup(_ => _.HasNotification()).Returns(true);
            _referralRepository.Setup(_ => _.AddAsync(It.Is<ReferralRegister>(__ => __.ReferralCode == "1010")))
                               .ReturnsAsync(new ReferralRegisterResponse());
            _referralRepository.Setup(_ => _.GetByCodeAsync("firstone@gmail.com"))
                               .ReturnsAsync(new Referral
                               {
                                   Id = anotherReferralId
                               });

            var result = await _referralService.EditAsync(referral);
            result.Should().BeNull();

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == "ReferralCode"
                 && __.Message == "Referral Code must be valid")), Times.Never);

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == "Email"
                 && __.Message == "Another referral with this e-mail already exists")), Times.Once);
        }

        [Fact]
        public async Task EditAsync_GivenReferral_WhenReposityReturnNotSuccessFullMessage_ShouldNotifyTheProblem()
        {
            string referralId = Guid.NewGuid().ToString();
            var referral = new ReferralRegister
            {
                ReferralCode = "1010",
                Email = "firstone@gmail.com",
                Id = referralId
            };

            _memberRepository.Setup(_ => _.GetByCodeAsync("1010"))
                             .ReturnsAsync(new Member());
            _notifier.Setup(_ => _.HasNotification()).Returns(false);
            _referralRepository.Setup(_ => _.EditAsync(It.Is<UpdateReferralInfo[]>(__ => __.Any(___ => ___.Query.PrimaryInfo.ReferralId == referralId))))
                               .ReturnsAsync(new UpdateReferralInfoResponse
                               {
                                   ResultInfo = new UpdateReferralResultInfo
                                   {
                                       Message = "Referral was not updated",
                                       Status = "NotSuccessed"
                                   }
                               });

            _referralRepository.Setup(_ => _.GetByCodeAsync(referralId))
                               .ReturnsAsync(new Referral
                               {
                                   Id = referralId
                               });

            var result = await _referralService.EditAsync(referral);
            result.Should().NotBeNull();

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == "ReferralCode"
                 && __.Message == "Referral Code must be valid")), Times.Never);

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == "Email"
                 && __.Message == "Another referral with this e-mail already exists")), Times.Never);

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == null
                 && __.Message == "Failed to update, reason: Referral was not updated")), Times.Once);
        }

        [Fact]
        public async Task EditAsync_GivenReferral_WhenReposityReturnSuccess_ShouldNotNotifyTheProblemAsItWasOK()
        {
            string referralId = Guid.NewGuid().ToString();
            var referral = new ReferralRegister
            {
                ReferralCode = "1010",
                Email = "firstone@gmail.com",
                Id = referralId
            };

            _memberRepository.Setup(_ => _.GetByCodeAsync("1010"))
                             .ReturnsAsync(new Member());
            _notifier.Setup(_ => _.HasNotification()).Returns(false);
            _referralRepository.Setup(_ => _.EditAsync(It.Is<UpdateReferralInfo[]>(__ => __.Any(___ => ___.Query.PrimaryInfo.ReferralId == referralId))))
                               .ReturnsAsync(new UpdateReferralInfoResponse
                               {
                                   ResultInfo = new UpdateReferralResultInfo
                                   {
                                       Message = "Referral was updated",
                                       Status = "Succeeded"
                                   }
                               });

            _referralRepository.Setup(_ => _.GetByCodeAsync(referralId))
                               .ReturnsAsync(new Referral
                               {
                                   Id = referralId
                               });

            var result = await _referralService.EditAsync(referral);
            result.Should().NotBeNull();

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == "ReferralCode"
                 && __.Message == "Referral Code must be valid")), Times.Never);

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == "Email"
                 && __.Message == "Another referral with this e-mail already exists")), Times.Never);

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ =>
                    __.Field == null
                 && __.Message == "Failed to update, reason: Referral was not updated")), Times.Never);
        }

        [Fact]
        public async Task RemoveAsync_WhenRepositoryNotReturnSuccessShouldNotifyTheProblem()
        {
            string referralId = Guid.NewGuid().ToString();

            _referralRepository.Setup(_ => _.GetByCodeAsync(referralId))
                               .ReturnsAsync(new Referral());

            _referralRepository.Setup(_ => _.RemoveAsync(It.Is<ReferralRemoveInfo[]>(__ => __.Any(___ => ___.Query.PrimaryInfo.ReferralId == referralId))))
                               .ReturnsAsync(new ReferralRemoveInfoResponse
                               {
                                   ResultInfo = new UpdateReferralResultInfo
                                   {
                                       Status = "NotSuccess",
                                       Message = "Referral was not removed"
                                   }
                               });

            await _referralService.RemoveAsync(referralId);

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ => __.Field == null && __.Message == "Failed to delete, reason: Referral was not removed")), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_GivenAnInexistingReferral_ShouldNotifyTheProblem()
        {
            string referralId = Guid.NewGuid().ToString();

            await _referralService.RemoveAsync(referralId);

            _referralRepository.Verify(_ => _.RemoveAsync(It.Is<ReferralRemoveInfo[]>(__ => __.Any(___ => ___.Query.PrimaryInfo.ReferralId == referralId))), Times.Never);
            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ => __.Field == "id" && __.Message == "ReferralId does not exist")), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_WhenRepositoryReturnSuccessShouldNotNotifyAsItWasOk()
        {
            string referralId = Guid.NewGuid().ToString();

            _referralRepository.Setup(_ => _.GetByCodeAsync(referralId))
                               .ReturnsAsync(new Referral());

            _referralRepository.Setup(_ => _.RemoveAsync(It.Is<ReferralRemoveInfo[]>(__ => __.Any(___ => ___.Query.PrimaryInfo.ReferralId == referralId))))
                               .ReturnsAsync(new ReferralRemoveInfoResponse
                               {
                                   ResultInfo = new UpdateReferralResultInfo
                                   {
                                       Status = "Success",
                                       Message = "Referral was removed"
                                   }
                               });

            await _referralService.RemoveAsync(referralId);

            _notifier.Verify(_ => _.Handle(It.Is<Notification>(__ => __.Field == null && __.Message == "Failed to delete, reason: Referral was removed")), Times.Never);
        }
    }
}