using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenStore.Infrastructure;
using OpenStore.Infrastructure.Email;
using OpenStore.Infrastructure.Email.Smtp;
using OpenStore.Infrastructure.Localization;
using OpenStore.Infrastructure.Tasks.InMemory;
using OpenStore.Omnichannel.Application;
using OpenStore.Omnichannel.Application.Command.ProductContext;
using OpenStore.Omnichannel.Infrastructure.Authentication;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework;
using OpenStore.Omnichannel.Infrastructure.ObjectStorage;
using OpenStore.Omnichannel.Projections;
using OpenStore.Omnichannel.ReadModel.Sql.Storefront;

namespace OpenStore.Omnichannel.Infrastructure;

public static class ServiceCollectionExtensions
{
    private const string KafkaConfigSectionKey = "kafka";
    private const string ElasticSearchConfigSectionKey = "elasticsearch";

    /// <summary>
    /// Adds memory cache, ef dbContext pool, identity data dependencies and authorization policies
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IMvcBuilder mvcBuilder,
        IHostEnvironment environment,
        IConfiguration configuration,
        bool withScheduledJobs = false)
    {
        var callingAssembly = Assembly.GetCallingAssembly();
        var infrastructureAssembly = Assembly.GetExecutingAssembly();
        var applicationAssembly = typeof(CreateProductMediaHandler).Assembly;
        var projectionsAssembly = typeof(OutBoxMessageHandler).Assembly;
        var readModelSqlAssembly = typeof(GetProductDetailByHandleQueryHandler).Assembly;

        services
            .AddProjections(
                configuration.GetSection(KafkaConfigSectionKey),
                configuration.GetSection(ElasticSearchConfigSectionKey)
            )
            .AddHttpContextAccessor()
            .AddMemoryCache()
            .AddEntityFrameworkInfrastructure(environment, configuration)
            .AddAuthorizationInfrastructure(configuration)
            .AddOpenStoreCore(callingAssembly, callingAssembly, applicationAssembly, infrastructureAssembly, projectionsAssembly, readModelSqlAssembly)
            .AddOpenStoreInMemoryBackgroundTasks()
            .AddOpenStoreMailInfrastructure(mailConf => { mailConf.UseSmtp(configuration, "Mail:Smtp"); })
            ;

        services.AddOpenStoreResxLocalization(mvcBuilder, options => { options.Assembly = callingAssembly; });

        services.AddTransient<IMessageDeliveryService, MessageDeliveryService>();
        services.AddSingleton<IObjectStorageService, FileSystemObjectStorageService>();

        if (withScheduledJobs)
        {
        }

        return services;
    }
}