using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Omnichannel.Domain.InventoryContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.InventoryContext;

public class InventoryEntityConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.HasOne(x => x.Variant)
            .WithOne(x => x.Inventory)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}