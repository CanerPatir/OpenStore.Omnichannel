using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OpenStore.Infrastructure.Logging;

namespace OpenStore.Omnichannel.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .AddOpenStoreLogging()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}