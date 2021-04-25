using System;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenStore.Omnichannel.Domain.ChannelContext;
using OpenStore.Omnichannel.Domain.IdentityContext;
using OpenStore.Omnichannel.Domain.LookupContext;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context
{
    public abstract class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IDataProtectionKeyContext
    {
        protected ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        // DataProtection
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        // Product context
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductMedia> ProductMedias { get; set; }

        // Channel context
        public DbSet<SaleChannel> SaleChannel { get; set; }
        public DbSet<SaleChannelProduct> SaleChannelProducts { get; set; }

        // Lookup Context
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandMedia> BrandMedias { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryMedia> CategoryMedias { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }

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