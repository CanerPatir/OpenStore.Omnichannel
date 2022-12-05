using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenStore.Data.EntityFramework;
using OpenStore.Data.OutBox;
using OpenStore.Omnichannel.Domain.ChannelContext;
using OpenStore.Omnichannel.Domain.CheckoutContext;
using OpenStore.Omnichannel.Domain.IdentityContext;
using OpenStore.Omnichannel.Domain.InventoryContext;
using OpenStore.Omnichannel.Domain.LookupContext;
using OpenStore.Omnichannel.Domain.OrderContext;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;

public abstract class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IOutBoxDbContext, IDataProtectionKeyContext
{
    protected ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    // Product context
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductMedia> ProductMedias { get; set; }
    public DbSet<ProductCollection> ProductCollections { get; set; }

    // Inventory context
    public DbSet<Inventory> Inventories { get; set; }

    // Channel context
    public DbSet<SaleChannel> SaleChannel { get; set; }
    public DbSet<SaleChannelProduct> SaleChannelProducts { get; set; }

    // Checkout Context
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    // Lookup Context
    public DbSet<Brand> Brands { get; set; }
    public DbSet<BrandMedia> BrandMedias { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryMedia> CategoryMedias { get; set; }
    public DbSet<CategoryProduct> CategoryProducts { get; set; }
    public DbSet<ApplicationUserAddress> UserAddresses { get; set; }

    // Oms Context
    public DbSet<Order> Orders { get; set; }

    // DataProtection
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

    public DbSet<OutBoxMessage> OutBoxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureSoftDelete();

        // Identity entities
        builder.Entity<ApplicationUser>(entity => entity.ToTable("Users"));
        builder.Entity<ApplicationRole>(entity => entity.ToTable("Roles"));
        builder.Entity<IdentityUserRole<Guid>>(entity => entity.ToTable("UserRoles"));
        builder.Entity<IdentityUserClaim<Guid>>(entity => entity.ToTable("UserClaims"));
        builder.Entity<IdentityUserLogin<Guid>>(entity => entity.ToTable("UserLogins"));
        builder.Entity<IdentityUserToken<Guid>>(entity => entity.ToTable("UserTokens"));
        builder.Entity<IdentityRoleClaim<Guid>>(entity => entity.ToTable("RoleClaims"));

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}