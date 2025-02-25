using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Data.EntityFramework.EntityConfiguration;
using OpenStore.Omnichannel.Domain.StoreContext;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.StoreContext;

public class StorePreferencesEntityConfiguration : BaseEntityTypeConfiguration<Guid, StorePreferences>
{
    public override void Configure(EntityTypeBuilder<StorePreferences> builder)
    {
        base.Configure(builder);

        builder.OwnsOne(x => x.Contact);
    }
}