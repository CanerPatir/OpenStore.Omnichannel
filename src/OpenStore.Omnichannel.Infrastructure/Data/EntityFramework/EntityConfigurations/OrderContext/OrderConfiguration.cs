using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 using OpenStore.Data.EntityFramework.EntityConfiguration;
using OpenStore.Omnichannel.Domain.OrderContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.OrderContext;

public class OrderConfiguration : BaseEntityTypeConfiguration<Guid, Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);
        builder.HasIndex(x => x.Number).IsUnique();
        builder.Property(x => x.Number).ValueGeneratedOnAdd();
        
        builder.Property(x => x.Tags).HasField("_tags");
        builder.Property(x => x.Discounts).HasField("_discounts");
        builder.Property(x => x.TimelineItems).HasField("_timelineItems");
        
        builder.Property(x => x.Tags).HasJsonConversion();
        builder.Property(x => x.Discounts).HasJsonConversion();
        builder.Property(x => x.TimelineItems).HasJsonConversion();

        builder.HasMany(x => x.Returns)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Fulfillments)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.LineItems)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(x => x.Customer);
        builder.OwnsOne(x => x.Payment);
        builder.OwnsOne(x => x.BillingAddress);
        builder.OwnsOne(x => x.ShippingAddress);
        builder.OwnsOne(x => x.ClientDetails);
        builder.OwnsOne(x => x.TotalPrice);
        builder.OwnsOne(x => x.TotalShippingPrice);
        builder.OwnsOne(x => x.TotalTax);
    }
}