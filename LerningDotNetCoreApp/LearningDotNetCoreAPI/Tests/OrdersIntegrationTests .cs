using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Xunit;

namespace Tests
{
    public class OrdersIntegrationTests:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public OrdersIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task GetOrders_ReturnsUnauthorized_WithoutToken()
        {
            var response = await _client.GetAsync("/api/orders");

            Xunit.Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        [Fact]
        public async Task Login_ThenGetOrders_ReturnsOk()
        {
            // Arrange — log in first to get a real token
            var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", new
            {
                username = "admin",
                password = "password123"
            });
            var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();

            // Attach the token to future requests
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResult!.Token);

            // Act
            var response = await _client.GetAsync("/api/orders");

            // Assert
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // Small helper record to deserialize the login response
        public record LoginResult(string Token);
    }
}
