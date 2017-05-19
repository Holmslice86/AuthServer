using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace AuthServer.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5000")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseIISIntegration()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
