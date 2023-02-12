using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Data.EntityFramework.EntityConfiguration;
using OpenStore.Omnichannel.Domain.OrderContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.OrderContext;

public class OrderLineItemEntityConfiguration : BaseEntityTypeConfiguration<Guid, OrderLineItem>
{
    public override void Configure(EntityTypeBuilder<OrderLineItem> builder)
    {
        base.Configure(builder);

        builder.OwnsOne(x => x.Price);
        builder.OwnsOne(x => x.Tax);

        builder.Property(x => x.TaxLines).HasField("_taxLines");

        builder.Property(x => x.TaxLines).HasJsonConversion();

        builder.HasMany(x => x.FulfillmentItems)
            .WithOne(x => x.OrderLineItem)
            .HasForeignKey(x => x.OrderLineItemId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.ReturnItems)
            .WithOne(x => x.OrderLineItem)
            .HasForeignKey(x => x.OrderLineItemId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}