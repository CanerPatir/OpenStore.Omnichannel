using System;
using System.IO;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using OpenStore.Omnichannel.Domain.IdentityContext;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;

namespace OpenStore.Omnichannel.Infrastructure.Authentication
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthorizationInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetIdentityConfiguration());

            services.AddAuthorization(options => { options.AddPolicy(ApplicationPolicies.AdministratorPolicy, policy => policy.RequireRole(ApplicationRoles.Administrator)); });

            // Register the Identity services.

            services
                .Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromDays(1))
                .Configure<IdentityOptions>(options =>
                {
                    options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
                    options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
                    options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
                })
                .Configure<SecurityStampValidatorOptions>(options =>
                {
                    // enables immediate logout, after updating the user's stat.
                    options.ValidationInterval = TimeSpan.Zero;
                })
                .AddIdentity<ApplicationUser, ApplicationRole>(opts =>
                {
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireDigit = true;
                    opts.Password.RequireUppercase = false;
                    var allowed = opts.User.AllowedUserNameCharacters + "ıiİüÜğçÇöÖşŞ";
                    opts.User.AllowedUserNameCharacters = allowed;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<MultilingualIdentityErrorDescriptor>()
                .AddDefaultTokenProviders()
                ;

            services.AddDataProtection()
                .SetApplicationName("OpenStore")
                // .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "DataProtection")))
                ;

            return services;
        }
    }
}