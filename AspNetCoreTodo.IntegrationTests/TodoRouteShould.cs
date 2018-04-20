using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreTodo.IntegrationTests
{
    public class TodoRouteShould : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;
        public TodoRouteShould(TestFixture testFixture)
        {
            _client = testFixture.Client;
        }

        [Fact]
        public async Task ChallengeAnonymousUser()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/todo");

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("https://localhost:5000/Account/Login?ReturnUrl=%2Ftodo", response.Headers.Location.ToString());
        }
    }
}