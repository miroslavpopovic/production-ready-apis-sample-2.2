using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace BoardGamesApi.Tests.IntegrationTests
{
    public class GamesApiTests
    {
        private readonly HttpClient _client;
        private readonly string _nonAdminToken;
        private readonly string _adminToken;

        public GamesApiTests()
        {
            const string issuer = "http://localhost:44303";
            const string key = "some-long-secret-key";

            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>().UseUrls("https://localhost:44303")
                .UseSetting("Tokens:Issuer", issuer)
                .UseSetting("Tokens:Key", key))
            {
                BaseAddress = new Uri("https://localhost:44303")
            };

            _client = server.CreateClient();

            _nonAdminToken = JwtTokenGenerator.Generate(
                "aspnetcore-api-demo", false, issuer, key);
            _adminToken = JwtTokenGenerator.Generate(
                "aspnetcore-api-demo", true, issuer, key);
        }

        [Fact]
        public async Task Delete_NoAuthorizationHeader_ReturnsUnauthorized()
        {
            _client.DefaultRequestHeaders.Clear();
            var result = await _client.DeleteAsync("/api/games/gam-000001");

            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public async Task Delete_NotAdmin_ReturnsForbidden()
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders
                .Add("Authorization", new[] { $"Bearer {_nonAdminToken}" });

            var result = await _client.DeleteAsync("/api/games/gam-000001");

            Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
        }

        [Fact]
        public async Task Delete_NoId_ReturnsMethodNotAllowed()
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders
                .Add("Authorization", new[] { $"Bearer {_adminToken}" });

            var result = await _client.DeleteAsync("/api/games/ ");

            Assert.Equal(HttpStatusCode.MethodNotAllowed, result.StatusCode);
        }

        [Fact]
        public async Task Delete_NonExistingId_ReturnsNotFound()
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders
                .Add("Authorization", new[] { $"Bearer {_adminToken}" });

            var result = await _client.DeleteAsync("/api/games/non-existing");

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Delete_ExistingId_ReturnsOk()
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders
                .Add("Authorization", new[] { $"Bearer {_adminToken}" });

            var result = await _client.DeleteAsync("/api/games/gam-000001");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
