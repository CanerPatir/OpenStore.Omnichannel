using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Data.EntityFramework.EntityConfiguration;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.ProductContext
{
    public class ProductMediaConfiguration : BaseEntityTypeConfiguration<Guid, ProductMedia>
    {
        public override void Configure(EntityTypeBuilder<ProductMedia> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.VariantIds).HasField("_variantIds");

            builder.Property(x => x.VariantIds)
                .HasConversion(
                    x => JsonSerializer.Serialize(x, new JsonSerializerOptions()),
                    x => JsonSerializer.Deserialize<HashSet<Guid>>(x, new JsonSerializerOptions())
                );
        }
    }
}