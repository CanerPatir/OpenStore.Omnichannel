using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Omnichannel.Domain.InventoryContext;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.InventoryContext;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.HasOne<Variant>(x => x.Variant)
            .WithOne(x => x.Inventory)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}