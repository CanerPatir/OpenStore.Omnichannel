using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OpenStore.Infrastructure.Logging;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Seeders;

namespace OpenStore.Omnichannel
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            DataSeeder.SeedAsync(host.Services).Wait();
            return host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .AddOpenStoreLogging()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}