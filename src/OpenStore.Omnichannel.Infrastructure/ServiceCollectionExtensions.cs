using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenStore.Infrastructure;
using OpenStore.Infrastructure.Email;
using OpenStore.Infrastructure.Localization;
using OpenStore.Infrastructure.Localization.Json;
using OpenStore.Infrastructure.Tasks.InMemory;
using OpenStore.Omnichannel.Infrastructure.Authentication;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework;

namespace OpenStore.Omnichannel.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds memory cache, ef dbContext pool, identity data dependencies and authorization policies
        /// </summary>
        public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services, IMvcBuilder mvcBuilder,
            IHostEnvironment environment,
            IConfiguration configuration,
            bool withScheduledJobs = false)
        {
            var callingAssembly = Assembly.GetCallingAssembly();
            var applicationAssembly = callingAssembly;
            var infrastructureAssembly = Assembly.GetExecutingAssembly();

            services
                .AddHttpContextAccessor()
                .AddMemoryCache()
                .AddEntityFrameworkInfrastructure(environment, configuration)
                .AddAuthorizationInfrastructure(configuration)
                .AddOpenStoreCore(callingAssembly, applicationAssembly, infrastructureAssembly)
                .AddOpenStoreInMemoryBackgroundTasks()
                ;
            
            services.AddOpenStoreResxLocalization(mvcBuilder, options =>
            {
                options.SharedResourceAssemblyName = callingAssembly.FullName;
            });

            if (withScheduledJobs)
            {
            }

            return services;
        }
    }
}