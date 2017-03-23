using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using AuthServer.Host;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Collections.Generic;
using Newtonsoft.Json;
using AuthServer.Tests.Models;

namespace AuthServer.Tests
{
    [TestClass]
    public class AuthenticationTests
    {
        private TestServer _server;

        [TestInitialize]
        public void Init()
        {
            var webHostBuilder = CreateWebHostBuilder();
            _server = new TestServer(webHostBuilder);
        }

        //[TestMethod]
        public async Task ClientExampleDoesntWorkInTest()
        {
            using (var client = _server.CreateClient())
            {
                var disco = await DiscoveryClient.GetAsync(client.BaseAddress.ToString());
                var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
                var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");
                Assert.AreEqual(false, tokenResponse.IsError);

                var disco2 = await DiscoveryClient.GetAsync(client.BaseAddress.ToString());
                var tokenClient2 = new TokenClient(disco.TokenEndpoint, "client", "secret");
                var tokenResponse2 = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");
                Assert.AreEqual(false, tokenResponse2.IsError);
            }
        }


        [TestMethod]
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
                Assert.IsTrue(responseMessage.IsSuccessStatusCode);
                Assert.IsTrue(!string.IsNullOrWhiteSpace(result.access_token));
            }
        }

        [TestMethod]
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

                Assert.IsTrue(responseMessage.IsSuccessStatusCode);
                Assert.IsTrue(!string.IsNullOrWhiteSpace(result.access_token));
            }
        }


        [TestMethod]
        public async Task GetUsers_ReturnsUsers()
        {
            using (var client = _server.CreateClient())
            {
                var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), "/api/User");

                var responseMessage = await client.SendAsync(requestMessage);

                var content = await responseMessage.Content.ReadAsStringAsync();

                Assert.IsFalse(string.IsNullOrWhiteSpace(content));
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
