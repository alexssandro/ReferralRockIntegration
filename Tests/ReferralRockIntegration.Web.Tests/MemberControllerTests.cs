using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReferralRockIntegration.ApiWrapper.Interfaces;
using ReferralRockIntegration.ApiWrapper.Models.Member;
using ReferralRockIntegration.Service.Interfaces;
using ReferralRockIntegration.Web.Controllers;

namespace ReferralRockIntegration.Web.Tests
{
    public class MemberControllerTests
    {
        private readonly MemberController _memberController;
        private readonly Mock<INotifier> _notifier;
        private readonly Mock<IMemberRepository> _memberRepository;

        public MemberControllerTests()
        {
            _notifier = new Mock<INotifier>();
            _memberRepository = new Mock<IMemberRepository>();
            _memberController = new MemberController(
                    _notifier.Object,
                    _memberRepository.Object);
        }

        [Fact]
        public async Task GivenAnyParameter_ShouldReturnTheMemberList()
        {
            var memberRequestParameter = new MemberRequestParameter { };

            _memberRepository.Setup(_ => _.SearchAsync(memberRequestParameter))
                             .ReturnsAsync(new MemberResponse
                             {
                                 Members = new Member[] {
                                     new Member { Id = "1", FirstName = "user 1" },
                                     new Member { Id = "2", FirstName = "user 2" },
                                     new Member { Id = "3", FirstName = "user 3" },
                                 }
                             });

            var result = await _memberController.Index(memberRequestParameter);

            result.Should().BeOfType<ViewResult>();

            var okResult = (ViewResult)result;
            okResult.Model.Should().BeOfType<MemberResponse>();
            var memberResponse = (MemberResponse)okResult.Model;
            memberResponse.Members.Length.Should().Be(3);
        }
    }
}