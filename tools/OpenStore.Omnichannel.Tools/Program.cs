using OpenStore.Application;
using OpenStore.Data.EntityFramework;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;
using OpenStore.Omnichannel.Tools;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // services.AddHostedService<DataGenerator>();
        services.AddHostedService<AddressImporter>();
        services.AddOpenStoreEfCore<ApplicationDbContext, SqliteDbContext>(hostContext.Configuration);
        services.AddSingleton<IOpenStoreUserContextAccessor, NullUserContextAccessor>();
    })
    .Build();

await host.RunAsync();

internal class NullUserContextAccessor : IOpenStoreUserContextAccessor
{
    public string GetUserEmail() => null;
}