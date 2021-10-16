using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Data.EntityFramework.EntityConfiguration;
using OpenStore.Omnichannel.Domain.InventoryContext;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.ProductContext;

public class VariantConfiguration : BaseEntityTypeConfiguration<Guid, Variant>
{
    public override void Configure(EntityTypeBuilder<Variant> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.Barcode);
        builder.HasIndex(x => x.Sku);

        builder.Property(x => x.Price).HasPrecision(18, 2);
        builder.Property(x => x.CompareAtPrice).HasPrecision(18, 2);
        builder.Property(x => x.Cost).HasPrecision(18, 2);

        builder.Property(x => x.Barcode).HasMaxLength(StringLengthConstants.DefaultStringLength);
        builder.Property(x => x.Sku).HasMaxLength(StringLengthConstants.DefaultStringLength);

        builder.HasOne<Inventory>(x => x.Inventory)
            .WithOne(x => x.Variant)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}