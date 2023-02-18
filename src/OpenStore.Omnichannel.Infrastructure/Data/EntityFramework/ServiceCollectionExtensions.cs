using System.Reflection;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenStore.Data.EntityFramework;
using OpenStore.Infrastructure.Mapping.AutoMapper;
using OpenStore.Omnichannel.Domain.IdentityContext;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Domain.StoreContext;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.Query.Management.StoreContext.Result;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEntityFrameworkInfrastructure(this IServiceCollection services,
        IHostEnvironment environment,
        IConfiguration configuration)
    {
        var migrationAssemblyName = Assembly.GetExecutingAssembly().FullName;

        var dataSource = configuration.GetActiveDataSource();

        void Opts(DbContextOptionsBuilder opts)
        {
            opts.UseLazyLoadingProxies();
            if (environment.IsDevelopment()) opts.EnableSensitiveDataLogging();

            opts.UseOpenIddict<ApplicationClient,
                ApplicationAuthorization,
                ApplicationScope,
                ApplicationToken, Guid>();
        }

        switch (dataSource)
        {
            case EntityFrameworkDataSource.SqLite:
                services.AddOpenStoreEfCoreWithReadReplica<ApplicationDbContext, SqliteDbContext, ReadOnlyApplicationDbContext, ReadOnlySqliteDbContext>(configuration, migrationAssemblyName, Opts);
                break;
            case EntityFrameworkDataSource.PostgreSql:
                services.AddOpenStoreEfCoreWithReadReplica<ApplicationDbContext, PostgresDbContext, ReadOnlyApplicationDbContext, ReadOnlyPostgresDbContext>(configuration, migrationAssemblyName, Opts);
                break;
            case EntityFrameworkDataSource.MySql:
                services.AddOpenStoreEfCoreWithReadReplica<ApplicationDbContext, MySqlDbContext, ReadOnlyApplicationDbContext, ReadOnlyMsSqlDbContext>(configuration, migrationAssemblyName, Opts);
                break;
            case EntityFrameworkDataSource.MsSql:
                services.AddOpenStoreEfCoreWithReadReplica<ApplicationDbContext, MsSqlDbContext, ReadOnlyApplicationDbContext, ReadOnlyMsSqlDbContext>(configuration, migrationAssemblyName, Opts);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        services
            .AddDataProtection()
            .PersistKeysToDbContext<ApplicationDbContext>();

        AddInMemoryCacheServiceProvider(services);

        // services.AddScoped(typeof(ICrudService<,>), typeof(CustomEntityFrameworkCrudService<,>));
        services.AddOpenStoreObjectMapper(cfg =>
        {
            // cfg.AddCollectionMappers();
            // cfg.UseEntityFrameworkCoreModel<ApplicationDbContext>(services.BuildServiceProvider().CreateScope().ServiceProvider);

            // to avoid unexpected lazy loading dto should be located to left hand of mapping
            cfg.CreateMap<ProductMediaDto, ProductMedia>().ReverseMap();
            cfg.CreateMap<ProductDto, Product>().ReverseMap();
            cfg.CreateMap<VariantDto, Variant>().ReverseMap()
                .ForMember(x => x.Quantity, opts => opts.MapFrom(v => v.Inventory != null ? v.Inventory.Quantity : 0))
                .ForMember(x => x.ContinueSellingWhenOutOfStock, opts => opts.MapFrom(v => v.Inventory != null && v.Inventory.ContinueSellingWhenOutOfStock))
                ;
            cfg.CreateMap<ProductOptionDto, ProductOption>().ReverseMap();
            cfg.CreateMap<ProductCollectionDto, ProductCollection>().ReverseMap();
            cfg.CreateMap<ProductCollectionMedia, ProductCollectionMediaDto>();

            cfg.CreateMap<StorePreferencesQueryResult, StorePreferences>().ReverseMap();
            cfg.CreateMap<StorePreferencesContactDto, StorePreferencesContact>().ReverseMap();
        });
        return services;
    }

    private static void AddInMemoryCacheServiceProvider(IServiceCollection services)
    {
        services.AddEFSecondLevelCache(opts => opts.UseMemoryCacheProvider(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(15)).DisableLogging());
    }
}