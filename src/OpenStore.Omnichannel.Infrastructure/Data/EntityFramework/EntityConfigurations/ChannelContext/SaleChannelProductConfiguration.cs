using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Omnichannel.Domain.ChannelContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.ChannelContext;

public class SaleChannelProductConfiguration : IEntityTypeConfiguration<SaleChannelProduct>
{
    public void Configure(EntityTypeBuilder<SaleChannelProduct> builder)
    {
        builder.HasKey(t => new { t.SaleChannelId, t.ProductId });

        builder
            .HasOne(x => x.SaleChannel)
            .WithMany()
            .HasForeignKey(x => x.SaleChannelId);

        builder
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId);
    }
}