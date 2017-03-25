using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using AuthServer.Host;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Collections.Generic;
using Newtonsoft.Json;
using AuthServer.Tests.Models;
using Xunit;
using Microsoft.AspNetCore.TestHost;

namespace AuthServer.Tests
{
    public class AuthenticationTests
    {
        private TestServer _server;

        public AuthenticationTests()
        {
            var webHostBuilder = CreateWebHostBuilder();
            _server = new TestServer(webHostBuilder);
        }

        //[Fact]
        public async Task ClientExampleDoesntWorkInTest()
        {
            using (var client = _server.CreateClient())
            {
                var disco = await DiscoveryClient.GetAsync(client.BaseAddress.ToString());
                var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
                var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");
                Assert.Equal(false, tokenResponse.IsError);

                var disco2 = await DiscoveryClient.GetAsync(client.BaseAddress.ToString());
                var tokenClient2 = new TokenClient(disco.TokenEndpoint, "client", "secret");
                var tokenResponse2 = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");
                Assert.Equal(false, tokenResponse2.IsError);
            }
        }


        [Fact]
        public async Task GetClientTestToken_ReturnsToken()
        {
           
            using (var client = _server.CreateClient())
            {
                var request = new HttpRequestMessage(new HttpMethod("POST"), "/connect/token");
                var dictionary = new Dictionary<string, string>();
                dictionary["client_id"] = "client";
                dictionary["client_secret"] = "secret";
                dictionary["grant_type"] = "client_credentials";
                dictionary["scope"] = "api1";
                request.Content = new FormUrlEncodedContent(dictionary);

                var responseMessage = await client.SendAsync(request);

                var content = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Token>(content);
                Assert.True(responseMessage.IsSuccessStatusCode);
                Assert.True(!string.IsNullOrWhiteSpace(result.access_token));
            }
        }

        [Fact]
        public async Task GetUserTestToken_ReturnsToken()
        {
            using (var client = _server.CreateClient())
            {
                var request = new HttpRequestMessage(new HttpMethod("POST"), "/connect/token");
                var dictionary = new Dictionary<string, string>();
                dictionary["client_id"] = "ro.client";
                dictionary["client_secret"] = "secret";
                dictionary["username"] = "alice";
                dictionary["password"] = "password";
                dictionary["grant_type"] = "password";
                dictionary["scope"] = "api1";
                request.Content = new FormUrlEncodedContent(dictionary);

                var responseMessage = await client.SendAsync(request);

                var content = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Token>(content);

                Assert.True(responseMessage.IsSuccessStatusCode);
                Assert.True(!string.IsNullOrWhiteSpace(result.access_token));
            }
        }


        [Fact]
        public async Task GetUsers_ReturnsUsers()
        {
            using (var client = _server.CreateClient())
            {
                var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), "/api/User");

                var responseMessage = await client.SendAsync(requestMessage);

                var content = await responseMessage.Content.ReadAsStringAsync();

                Assert.False(string.IsNullOrWhiteSpace(content));
            }
        }

        private IWebHostBuilder CreateWebHostBuilder()
        {
            var config = new ConfigurationBuilder().Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseStartup<Startup>();

            return host;
        }
    }
}
