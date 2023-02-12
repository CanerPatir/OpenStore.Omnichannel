using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Omnichannel.Domain.IdentityContext;
using static OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.StringLengthConstants;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.IdentityContext;

public class ApplicationScopeEntityConfiguration : IEntityTypeConfiguration<ApplicationScope>
{
    public void Configure(EntityTypeBuilder<ApplicationScope> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(_191_);
    }
}