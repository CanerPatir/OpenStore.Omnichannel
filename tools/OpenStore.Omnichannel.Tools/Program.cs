using OpenStore.Application;
using OpenStore.Data.EntityFramework;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;
using OpenStore.Omnichannel.Tools;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<DataGenerator>();
        services.AddOpenStoreEfCore<ApplicationDbContext, SqliteDbContext>(hostContext.Configuration);
        services.AddSingleton<IOpenStoreUserContextAccessor, NullUserContextAccessor>();
    })
    .Build();

await host.RunAsync();

class NullUserContextAccessor : IOpenStoreUserContextAccessor
{
    public string GetUserEmail() => null;
}