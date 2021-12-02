using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.ProductContext;

public class ProductCollectionItemConfiguration : IEntityTypeConfiguration<ProductCollectionItem>
{
    public void Configure(EntityTypeBuilder<ProductCollectionItem> builder)
    {
        builder.HasKey(t => new { t.ProductCollectionId, t.ProductId });

        builder
            .HasOne(x => x.ProductCollection)
            .WithMany(x => x.ProductItems)
            .HasForeignKey(x => x.ProductCollectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}