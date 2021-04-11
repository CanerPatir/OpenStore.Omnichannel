using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenStore.Omnichannel.Domain.IdentityContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context
{
    public abstract class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid> //, IDataProtectionKeyContext
    {
        protected ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        // Identity
        // public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureSoftDelete();

            // Identity entities
            builder.Entity<ApplicationUser>(entity => entity.ToTable(name: "Users"));
            builder.Entity<ApplicationRole>(entity => entity.ToTable(name: "Roles"));
            builder.Entity<IdentityUserRole<Guid>>(entity => entity.ToTable("UserRoles"));
            builder.Entity<IdentityUserClaim<Guid>>(entity => entity.ToTable("UserClaims"));
            builder.Entity<IdentityUserLogin<Guid>>(entity => entity.ToTable("UserLogins"));
            builder.Entity<IdentityUserToken<Guid>>(entity => entity.ToTable("UserTokens"));
            builder.Entity<IdentityRoleClaim<Guid>>(entity => entity.ToTable("RoleClaims"));

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}