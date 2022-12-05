using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Data.EntityFramework.EntityConfiguration;
using OpenStore.Omnichannel.Domain.OrderContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.OrderContext;

public class FulfillmentConfiguration : BaseEntityTypeConfiguration<Guid, Fulfillment>
{
    public override void Configure(EntityTypeBuilder<Fulfillment> builder)
    {
        base.Configure(builder);

        builder.OwnsOne(x => x.ShippingAddress);

        builder.HasMany(x => x.FulfillmentItems)
            .WithOne(x => x.Fulfillment)
            .HasForeignKey(x => x.FulfillmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}