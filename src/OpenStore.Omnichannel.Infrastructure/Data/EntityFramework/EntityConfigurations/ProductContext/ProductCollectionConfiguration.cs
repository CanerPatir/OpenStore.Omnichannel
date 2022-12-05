using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Data.EntityFramework.EntityConfiguration;
using OpenStore.Omnichannel.Domain.ProductContext;
using static OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.StringLengthConstants;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.ProductContext;

public class ProductCollectionConfiguration : BaseEntityTypeConfiguration<Guid, ProductCollection>
{
    public override void Configure(EntityTypeBuilder<ProductCollection> builder)
    {
        base.Configure(builder);
        builder.HasIndex(x => x.Handle).IsUnique();

        builder.Property(x => x.Handle).IsRequired().HasMaxLength(MediumStringLength);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(DefaultStringLength);

        builder.OwnsOne(p => p.Media);
    }
}