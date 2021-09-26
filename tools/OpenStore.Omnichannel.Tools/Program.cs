using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenStore.Application;
using OpenStore.Domain;
using OpenStore.Data.EntityFramework;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;

namespace OpenStore.Omnichannel.Tools
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<DataGenerator>();
                    services.AddOpenStoreEfCore<ApplicationDbContext, SqliteDbContext>(hostContext.Configuration);
                    services.AddSingleton<IOpenStoreUserContextAccessor, NullUserContextAccessor>();
                });
    }

    class NullUserContextAccessor : IOpenStoreUserContextAccessor
    {
        public string GetUserEmail() => null;
    }
}