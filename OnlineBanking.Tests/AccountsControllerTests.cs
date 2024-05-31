using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace OnlineBanking.Tests
{
    public class AccountsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AccountsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost:7223")
            });
        }

        [Fact]
        public async Task CreateAccount_ReturnsOk()
        {
            // Arrange
            var account = new { AccountHolderName = "Erdogan3", accountNumber = "E123", Balance = 200 };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/accounts/create", account);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Deposit_ReturnsOk()
        {
            // Arrange
            var deposit = new { Amount = 100 };
            var accountId = 1;

            // Act
            var response = await _client.PostAsJsonAsync($"/api/v1/accounts/deposit", deposit);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Withdraw_ReturnsOk()
        {
            // Arrange
            var withdraw = new { Amount = 50 };
            var accountId = 1;

            // Act
            var response = await _client.PostAsJsonAsync($"/api/v1/accounts/withdraw", withdraw);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }

    public class Startup
    {
        public Startup(IServiceCollection services)
        {

        }

        public void Configure(IApplicationBuilder app)
        {
        }
    }
}
