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

namespace AuthServer.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestInitialize]
        public void Init()
        {

        }

        //[TestMethod]
        public async Task WorksButNotInTest()
        {
            var webHostBuilder = CreateWebHostBuilder();
            var server = new TestServer(webHostBuilder);
            using (var client = server.CreateClient())
            {
                var disco = await DiscoveryClient.GetAsync(client.BaseAddress.ToString());
                var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
                var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");
                Assert.AreEqual(false, tokenResponse.IsError);
            }
        }


        [TestMethod]
        public async Task GetTestToken_ReturnsToken()
        {
            var webHostBuilder = CreateWebHostBuilder();
            var server = new TestServer(webHostBuilder);
            using (var client = server.CreateClient())
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

            }
        }


        [TestMethod]
        public async Task TestMethod1()
        {
            var webHostBuilder = CreateWebHostBuilder();
            var server = new TestServer(webHostBuilder);

            using (var client = server.CreateClient())
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
