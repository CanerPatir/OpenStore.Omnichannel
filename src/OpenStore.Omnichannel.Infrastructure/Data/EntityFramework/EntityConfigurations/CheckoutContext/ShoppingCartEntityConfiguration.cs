using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Data.EntityFramework.EntityConfiguration;
using OpenStore.Omnichannel.Domain.CheckoutContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.CheckoutContext;

public class ShoppingCartEntityConfiguration : BaseEntityTypeConfiguration<Guid, ShoppingCart>
{
    public override void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        base.Configure(builder);
        builder.HasIndex(x => x.UserId).IsUnique();

        builder.Property(x => x.Items).HasField("_items");

        builder.Property(x => x.Items)
            .HasJsonConversion();
    }
}