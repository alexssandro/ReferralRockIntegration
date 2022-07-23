using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ReferralRockIntegration.ApiWrapper.Tests
{
    internal static class HttpClientConfiguration
    {
        public static Mock<HttpMessageHandler> CreateHttpClient<T>(this Mock<IHttpClientFactory> httpClientFactory,T objecToSerialize, HttpStatusCode httpStatusCode) where T : class
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = httpStatusCode,
                    Content = new StringContent(JsonSerializer.Serialize(objecToSerialize), Encoding.UTF8, "application/json"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost")
            };

            httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>()))
                             .Returns(client);       

            return mockHttpMessageHandler;
        }
    }
}
