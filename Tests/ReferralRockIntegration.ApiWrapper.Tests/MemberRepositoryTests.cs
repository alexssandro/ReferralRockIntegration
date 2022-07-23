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
        public async Task GivenNoExtraParameters_ShouldInvokePassingOnlyProgramId()
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
        public async Task GivenOnlyQuery_ShouldInvokePassingOnlyProgramIdAndQuery()
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
        public async Task GivenOnlyShowDisabled_ShouldInvokePassingOnlyProgramIdAndQuery()
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
        public async Task GivenOnlySort_ShouldInvokePassingOnlyProgramIdAndQuery()
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
        public async Task GivenOnlydateFrom_ShouldInvokePassingOnlyProgramIdAndQuery()
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
                            req.RequestUri.PathAndQuery == "/api/members?programId=123456&dateFrom=7/23/2022%2012:00:00%20AM"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }
    }
}