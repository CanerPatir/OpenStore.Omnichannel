using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Data.EntityFramework.EntityConfiguration;
using OpenStore.Omnichannel.Domain.LookupContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.LookupContext;

public class BrandConfiguration : BaseEntityTypeConfiguration<Guid, Brand>
{
    public override void Configure(EntityTypeBuilder<Brand> builder)
    {
        base.Configure(builder);

        builder.HasMany(x => x.Medias)
            .WithOne(x => x.Brand)
            .HasForeignKey(x => x.BrandId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}