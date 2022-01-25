using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Data.EntityFramework.EntityConfiguration;
using OpenStore.Omnichannel.Domain.OrderContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.OrderContext;

public class ReturnConfiguration : BaseEntityTypeConfiguration<Guid, Return>
{
    public override void Configure(EntityTypeBuilder<Return> builder)
    {
        base.Configure(builder);

        builder.HasMany(x => x.ReturnItems)
            .WithOne(x => x.Return)
            .HasForeignKey(x => x.ReturnId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}