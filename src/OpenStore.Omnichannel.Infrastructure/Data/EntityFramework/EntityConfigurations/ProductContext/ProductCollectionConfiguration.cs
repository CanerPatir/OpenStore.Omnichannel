using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Data.EntityFramework.EntityConfiguration;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.ProductContext;

public class ProductCollectionConfiguration : BaseEntityTypeConfiguration<Guid, ProductCollection>
{
    public override void Configure(EntityTypeBuilder<ProductCollection> builder)
    {
        base.Configure(builder);
 
    }
}