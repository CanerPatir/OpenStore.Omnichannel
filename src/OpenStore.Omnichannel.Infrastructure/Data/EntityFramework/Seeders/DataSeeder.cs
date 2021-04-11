using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenStore.Omnichannel.Domain.IdentityContext;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Seeders
{
    public static class DataSeeder
    {
        private static readonly Guid DefaultAdminId = new("a2a9a91d-1019-473a-8750-59f51ef0c61a");
        private const string DefaultAdminUserEmail = "admin@openstore.com";
        private const string DefaultAdminUserPassword = "Qwer1234";
        private static readonly string[] DefaultRoles = ApplicationRoles.AsArray;

        public static async Task SeedAsync(IServiceProvider hostServices)
        {
            using var scope = hostServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                await SeedInternal(services);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        private static async Task SeedInternal(IServiceProvider services)
        {
            await using var context = services.GetRequiredService<ApplicationDbContext>();
            using var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
            using var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            // await context.Database.EnsureCreatedAsync();
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            try
            {
                if (pendingMigrations.Any()) await context.Database.MigrateAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await SeedEssentialData(context, roleManager, userManager);
        }

        private static async Task SeedEssentialData(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            // await SeedNewInstructors(context, roleManager, userManager);

            if (await context.Users.AnyAsync()) return;

            await CreateDefaultRoles(roleManager);
            var defaultAdminUser = await CreateDefaultAdminUser(userManager);
            await AddDefaultAdminRoleToDefaultAdminUser(userManager, defaultAdminUser);
        }


        private static async Task CreateDefaultRoles(RoleManager<ApplicationRole> roleManager)
        {
            // Make sure we have an Administrator role
            foreach (var defaultRole in DefaultRoles)
            {
                var role = new ApplicationRole
                {
                    Name = defaultRole
                };

                var roleResult = await roleManager.CreateAsync(role);
                if (!roleResult.Succeeded) throw new ApplicationException($"Could not create '{defaultRole}' role");
            }
        }

        private static async Task<ApplicationUser> CreateDefaultAdminUser(UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser()
            {
                Id = DefaultAdminId,
                UserName = DefaultAdminUserEmail,
                Name = "Admin",
                Surname = "Admin",
                Email = DefaultAdminUserEmail,
                EmailConfirmed = true
            };
            var userResult = await userManager.CreateAsync(user, DefaultAdminUserPassword);

            if (!userResult.Succeeded) throw new ApplicationException($"Could not create '{DefaultAdminUserEmail}' user");

            return user;
        }

        private static async Task AddDefaultAdminRoleToDefaultAdminUser(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            foreach (var defaultAdminRoleName in DefaultRoles)
                // Add user to Administrator role if it's not already associated
                if (!(await userManager.GetRolesAsync(user)).Contains(defaultAdminRoleName))
                {
                    var addToRoleResult = await userManager.AddToRoleAsync(user, defaultAdminRoleName);
                    if (!addToRoleResult.Succeeded) throw new ApplicationException($"Could not add user '{DefaultAdminUserEmail}' to '{defaultAdminRoleName}' role");
                }
        }
    }
}