using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using ReferralRockIntegration.ApiWrapper.Models.Configuration;
using ReferralRockIntegration.ApiWrapper.Models.Member;
using System.Net;
using System.Threading;

namespace ReferralRockIntegration.ApiWrapper.Tests
{
    public class MemberRepositoryTests
    {
        private readonly MemberRepository _memberRepository;
        private readonly Mock<IHttpClientFactory> _httpClientFactory;
        private readonly ReferralRockConfiguration _referralRockConfiguration;
        private readonly Mock<ILogger<MemberRepository>> _logger;

        public MemberRepositoryTests()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _referralRockConfiguration = new ReferralRockConfiguration();
            _logger = new Mock<ILogger<MemberRepository>>();

            _memberRepository = new MemberRepository(
                                        _httpClientFactory.Object,
                                        _referralRockConfiguration,
                                        _logger.Object);
        }

        [Fact]
        public async Task SearchAsync_GivenNoExtraParameters_ShouldInvokePassingOnlyProgramId()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new MemberResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new MemberRequestParameter { };

            var result = await _memberRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/members?programId=123456"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlyQuery_ShouldInvokePassingOnlyProgramIdAndQuery()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new MemberResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new MemberRequestParameter
            {
                Query = "abc123"
            };

            var result = await _memberRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/members?programId=123456&query=abc123"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlyShowDisabled_ShouldInvokePassingOnlyProgramIdAlongside()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new MemberResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new MemberRequestParameter
            {
                ShowDisabled = true
            };

            var result = await _memberRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/members?programId=123456&showDisabled=True"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlySort_ShouldInvokePassingOnlyProgramIdAlongside()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new MemberResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new MemberRequestParameter
            {
                Sort = "firstName"
            };

            var result = await _memberRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/members?programId=123456&sort=firstName"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlydateFrom_ShouldInvokePassingOnlyProgramIdAlongside()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new MemberResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new MemberRequestParameter
            {
                DateFrom = DateTime.Parse("2022-07-23")
            };

            var result = await _memberRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/members?programId=123456&dateFrom=07/23/2022%2000:00:00"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlydateTo_ShouldInvokePassingOnlyProgramIdAlongside()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new MemberResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new MemberRequestParameter
            {
                DateTo = DateTime.Parse("2022-07-22")
            };

            var result = await _memberRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/members?programId=123456&dateTo=07/22/2022%2000:00:00"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlyOffset_ShouldInvokePassingOnlyProgramIdAlongside()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new MemberResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new MemberRequestParameter
            {
                OffSet = 7
            };

            var result = await _memberRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/members?programId=123456&offset=7"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlyCount_ShouldInvokePassingOnlyProgramIdAlongside()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new MemberResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new MemberRequestParameter
            {
                Count = 10
            };

            var result = await _memberRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/members?programId=123456&count=10"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenSortShowDisabledAndQuery_ShouldInvokePassingProgramIdAlongsideToThem()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new MemberResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new MemberRequestParameter
            {
                ShowDisabled = false,
                Sort = "email",
                Query = "12345678"
            };

            var result = await _memberRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/members?programId=123456&query=12345678&showDisabled=False&sort=email"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetByCodeAsync_GivenTheCode_ShouldInvokePassingThisCode()
        {
            string code = "abc123";
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new MemberResponse 
            {
                Members = new Member[]
                {
                    new Member { FirstName = "First", LastName = "one" }
                }
            }, HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var result = await _memberRepository.GetByCodeAsync(code);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/members?programId=123456&query=abc123"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());

            result.Should().NotBeNull();
            result.FirstName.Should().Be("First");
        }
    }
}