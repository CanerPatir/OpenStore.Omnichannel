using System;
using System.Reflection;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenStore.Infrastructure.Data.EntityFramework;
using OpenStore.Infrastructure.Mapping.AutoMapper;
using OpenStore.Omnichannel.Domain.IdentityContext;
using OpenStore.Omnichannel.Domain.MediaContext;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;
using OpenStore.Omnichannel.Shared.Dto.Media;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework
{
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
                if (environment.IsDevelopment())
                {
                    opts.EnableSensitiveDataLogging();
                }

                opts.UseOpenIddict<ApplicationClient,
                    ApplicationAuthorization,
                    ApplicationScope,
                    ApplicationToken, Guid>();
            }

            switch (dataSource)
            {
                case EntityFrameworkDataSource.SqLite:
                    services.AddOpenStoreEfCore<ApplicationDbContext, SqliteDbContext>(configuration, migrationAssembly: migrationAssemblyName, optionsBuilder: Opts);
                    break;
                case EntityFrameworkDataSource.PostgreSql:
                    services.AddOpenStoreEfCore<ApplicationDbContext, PostgreSqlDbContext>(configuration, migrationAssembly: migrationAssemblyName, optionsBuilder: Opts);
                    break;
                case EntityFrameworkDataSource.MySql:
                    services.AddOpenStoreEfCore<ApplicationDbContext, MySqlDbContext>(configuration, migrationAssembly: migrationAssemblyName, optionsBuilder: Opts);
                    break;
                case EntityFrameworkDataSource.MsSql:
                    services.AddOpenStoreEfCore<ApplicationDbContext, MsSqlDbContext>(configuration, migrationAssembly: migrationAssemblyName, optionsBuilder: Opts);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            AddInMemoryCacheServiceProvider(services);

            // services.AddScoped(typeof(ICrudService<,>), typeof(CustomEntityFrameworkCrudService<,>));
            services.AddOpenStoreObjectMapper(cfg =>
            {
                // cfg.AddCollectionMappers();
                // cfg.UseEntityFrameworkCoreModel<ApplicationDbContext>(services.BuildServiceProvider().CreateScope().ServiceProvider);

                // to avoid unexpected lazy loading dto should be located to left hand of mapping
                // identity
                cfg.CreateMap<MediaDto, Media>().ReverseMap();
                
            });
            return services;
        }

        private static void AddInMemoryCacheServiceProvider(IServiceCollection services)
        {
            services.AddEFSecondLevelCache(opts => opts.UseMemoryCacheProvider(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(15)).DisableLogging());
        }
    }
}