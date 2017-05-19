using System.Net.Http;
using System.Threading.Tasks;
using AuthServer.Host;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace AuthServer.Users.Test.Integration
{
    public class UserTests
    {
        private TestServer _server;

        public UserTests()
        {
            var webHostBuilder = CreateWebHostBuilder();
            _server = new TestServer(webHostBuilder);
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
