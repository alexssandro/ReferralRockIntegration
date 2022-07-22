using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace ReferralRockIntegration.Web.IntegrationTests
{
    public class HealthCheck : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public HealthCheck(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateDefaultClient();
        }

        [Fact]
        public async Task HealthCheck_ReturnsOk()
        {
            var response = await _httpClient.GetAsync("/healthcheck");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}