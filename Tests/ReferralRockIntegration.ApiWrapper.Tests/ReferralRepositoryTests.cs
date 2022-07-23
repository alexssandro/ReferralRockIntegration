
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using ReferralRockIntegration.ApiWrapper.Models.Configuration;
using ReferralRockIntegration.ApiWrapper.Models.Entitiy.Referrals;
using ReferralRockIntegration.ApiWrapper.Models.Referrals;
using System.Net;

namespace ReferralRockIntegration.ApiWrapper.Tests
{
    public class ReferralRepositoryTests
    {
        private readonly ReferralRepository _referralRepository;
        private readonly Mock<IHttpClientFactory> _httpClientFactory;
        private readonly ReferralRockConfiguration _referralRockConfiguration;
        private readonly Mock<ILogger<ReferralRepository>> _logger;

        public ReferralRepositoryTests()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _referralRockConfiguration = new ReferralRockConfiguration();
            _logger = new Mock<ILogger<ReferralRepository>>();
            _referralRepository = new ReferralRepository(
                _httpClientFactory.Object,
                _referralRockConfiguration,
                _logger.Object);
        }

        [Fact]
        public async Task SearchAsync_GivenNoExtraParameters_ShouldInvokePassingOnlyProgramId()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new ReferralResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var referralRequest = new ReferralRequestParameter { };

            var result = await _referralRepository.SearchAsync(referralRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/referrals?programId=123456"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlyQuery_ShouldInvokePassingOnlyProgramIdAndQuery()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new ReferralResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new ReferralRequestParameter
            {
                Query = "abc123"
            };

            var result = await _referralRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/referrals?programId=123456&query=abc123"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlySort_ShouldInvokePassingOnlyProgramIdAlongside()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new ReferralResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new ReferralRequestParameter
            {
                Sort = "firstName"
            };

            var result = await _referralRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/referrals?programId=123456&sort=firstName"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlydateFrom_ShouldInvokePassingOnlyProgramIdAlongside()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new ReferralResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new ReferralRequestParameter
            {
                DateFrom = DateTime.Parse("2022-07-23")
            };

            var result = await _referralRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/referrals?programId=123456&dateFrom=07/23/2022%2000:00:00"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlydateTo_ShouldInvokePassingOnlyProgramIdAlongside()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new ReferralResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new ReferralRequestParameter
            {
                DateTo = DateTime.Parse("2022-07-22")
            };

            var result = await _referralRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/referrals?programId=123456&dateTo=07/22/2022%2000:00:00"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlyOffset_ShouldInvokePassingOnlyProgramIdAlongside()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new ReferralResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new ReferralRequestParameter
            {
                OffSet = 7
            };

            var result = await _referralRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/referrals?programId=123456&offset=7"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlyCount_ShouldInvokePassingOnlyProgramIdAlongside()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new ReferralResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new ReferralRequestParameter
            {
                Count = 10
            };

            var result = await _referralRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/referrals?programId=123456&count=10"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlyMemberId_ShouldInvokePassingOnlyProgramIdAlongside()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new ReferralResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";
            string memberId = Guid.NewGuid().ToString();

            var memberRequest = new ReferralRequestParameter
            {
                MemberId = memberId
            };

            var result = await _referralRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == $"/api/referrals?programId=123456&memberId={memberId}"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenOnlyStatus_ShouldInvokePassingOnlyProgramIdAlongside()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new ReferralResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new ReferralRequestParameter
            {
                Status = "closed"
            };

            var result = await _referralRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/referrals?programId=123456&status=closed"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task SearchAsync_GivenSortAndQuery_ShouldInvokePassingProgramIdAlongsideToThem()
        {
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(new ReferralResponse(), HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var memberRequest = new ReferralRequestParameter
            {
                Sort = "email",
                Query = "12345678"
            };

            var result = await _referralRepository.SearchAsync(memberRequest);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/referrals?programId=123456&query=12345678&sort=email"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetByCodeAsync_GivenTheCode_ShouldInvokePassingThisCode()
        {
            string code = "abc123";
            var httpMessageHandler = _httpClientFactory.CreateHttpClient(
                new Referral { FirstName = "First", LastName = "one" }, HttpStatusCode.OK);
            _referralRockConfiguration.ProgramId = "123456";

            var result = await _referralRepository.GetByCodeAsync(code);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/referral/getsingle?referralQuery=abc123"
                         && req.Method == HttpMethod.Get
                    ),
                    ItExpr.IsAny<CancellationToken>());

            result.Should().NotBeNull();
            result.FirstName.Should().Be("First");
        }

        [Fact]
        public async Task AddAsync_GivenUserRegister_ShouldInvokeCreate()
        {
            var referralRegister = new ReferralRegister
            {
                Email = "firstone@gmail.com",
                FirstName = "First",
                LastName = "One",
                ReferralCode = "1010"
            };

            var httpMessageHandler = _httpClientFactory.CreateHttpClient(
                new ReferralRegisterResponse
                {
                    Referral = new Referral
                    {
                        FirstName = "First",
                        LastName = "one",
                        Email = "firstone@gmail.com"
                    }
                }, HttpStatusCode.OK);

            _referralRockConfiguration.ProgramId = "123456";

            await _referralRepository.AddAsync(referralRegister);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/referrals"
                         && req.Method == HttpMethod.Post
                         && req.Content.ReadAsStringAsync().Result == "{\"id\":null,\"referralCode\":\"1010\",\"firstName\":\"First\",\"lastName\":\"One\",\"email\":\"firstone@gmail.com\"}"
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task EditAsync_GivenUserRegister_ShouldInvokeEdit()
        {
            string referralId = Guid.NewGuid().ToString();

            var updateReferralInfo = new UpdateReferralInfo[]
            {
                new UpdateReferralInfo
                {
                    Query = new ReferralQuery
                    {
                        PrimaryInfo = new Primaryinfo
                        {
                            ReferralId = referralId
                        }
                    },
                    Referral = new ReferralRegister
                    {
                        Id = referralId,
                        Email = "firstone@gmail.com",
                        FirstName = "First",
                        LastName = "One",
                        ReferralCode = "1010"
                    }
                }
            };

            var httpMessageHandler = _httpClientFactory.CreateHttpClient(
                new UpdateReferralInfoResponse[]
                {
                    new UpdateReferralInfoResponse
                    {
                        Query = new ReferralQuery
                        {
                            PrimaryInfo = new Primaryinfo
                            {
                                ReferralId = referralId
                            }
                        },
                        Referral = new Referral
                        {
                            Id = referralId,
                            FirstName = "First",
                            LastName = "one",
                            Email = "firstone@gmail.com"
                        },
                        ResultInfo = new UpdateReferralResultInfo
                        {
                            Status = "Successed",
                            Message = "Referral was successfully updated"
                        }
                    }
                }
                , HttpStatusCode.OK);

            _referralRockConfiguration.ProgramId = "123456";

            await _referralRepository.EditAsync(updateReferralInfo);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/referral/update"
                         && req.Method == HttpMethod.Post
                         && req.Content.ReadAsStringAsync().Result == "[{\"query\":{\"primaryInfo\":{\"referralId\":\"" + referralId + "\"}},\"referral\":{\"id\":\"" + referralId + "\",\"referralCode\":\"1010\",\"firstName\":\"First\",\"lastName\":\"One\",\"email\":\"firstone@gmail.com\"}}]"
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task RemoveAsync_GivenUserRegister_ShouldInvokeRemote()
        {
            string referralId = Guid.NewGuid().ToString();

            var referralRemoveInfo = new ReferralRemoveInfo[]
            {
                new ReferralRemoveInfo
                {
                    Query = new ReferralQuery
                    {
                        PrimaryInfo = new Primaryinfo
                        {
                            ReferralId = referralId
                        }
                    },
                }
            };

            var httpMessageHandler = _httpClientFactory.CreateHttpClient(
                new ReferralRemoveInfoResponse[]
                {
                    new ReferralRemoveInfoResponse
                    {
                        Query = new ReferralQuery
                        {
                            PrimaryInfo = new Primaryinfo
                            {
                                ReferralId = referralId
                            }
                        },
                        ResultInfo = new UpdateReferralResultInfo
                        {
                            Status = "Successed",
                            Message = "Referral was successfully removed"
                        }
                    }
                }
                , HttpStatusCode.OK);

            _referralRockConfiguration.ProgramId = "123456";

            await _referralRepository.RemoveAsync(referralRemoveInfo);

            httpMessageHandler.Protected().Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req =>
                            req.RequestUri.PathAndQuery == "/api/referral/remove"
                         && req.Method == HttpMethod.Post
                         && req.Content.ReadAsStringAsync().Result == "[{\"query\":{\"primaryInfo\":{\"referralId\":\"" + referralId + "\"}}}]"
                    ),
                    ItExpr.IsAny<CancellationToken>());
        }
    }
}
