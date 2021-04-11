using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Omnichannel.Domain.IdentityContext;
using static OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.StringLengthConstants;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("Roles");
            builder.Property(x => x.Name).HasMaxLength(_191_);
            builder.Property(x => x.NormalizedName).HasMaxLength(_191_);
        }
    }
}