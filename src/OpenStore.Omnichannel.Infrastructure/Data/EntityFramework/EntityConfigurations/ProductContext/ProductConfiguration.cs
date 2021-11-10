using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Data.EntityFramework.EntityConfiguration;
using OpenStore.Omnichannel.Domain.ProductContext;
using static OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.StringLengthConstants;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.ProductContext;

public class ProductConfiguration : BaseEntityTypeConfiguration<Guid, Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.Handle).IsUnique();

        builder.Property(x => x.Handle).IsRequired().HasMaxLength(MediumStringLength);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(DefaultStringLength);
        builder.Property(x => x.Options).HasField("_options");
        // builder.Property(x => x.Medias).HasField("_medias");
        // builder.Property(x => x.Variants).HasField("_variants");
        builder.Property(x => x.Weight).HasPrecision(9, 2);

        builder.Property(x => x.Options)
            .HasConversion(
                x => JsonSerializer.Serialize(x, new JsonSerializerOptions()),
                x => JsonSerializer.Deserialize<List<ProductOption>>(x, new JsonSerializerOptions())
            );

        builder.HasMany(x => x.Medias)
            .WithOne()
            .HasForeignKey(x => x.ProductId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Variants)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
    }
}