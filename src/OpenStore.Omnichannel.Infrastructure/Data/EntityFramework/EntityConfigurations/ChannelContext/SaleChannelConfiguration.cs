using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Data.EntityFramework.EntityConfiguration;
using OpenStore.Omnichannel.Domain.ChannelContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.ChannelContext;

public class SaleChannelConfiguration : BaseEntityTypeConfiguration<Guid, SaleChannel>
{
    public override void Configure(EntityTypeBuilder<SaleChannel> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).HasMaxLength(StringLengthConstants.DefaultStringLength).IsRequired();
    }
}