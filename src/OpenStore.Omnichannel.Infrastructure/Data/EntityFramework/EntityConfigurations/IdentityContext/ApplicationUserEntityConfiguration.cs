using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenStore.Omnichannel.Domain.IdentityContext;
using static OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.StringLengthConstants;

namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.EntityConfigurations.IdentityContext;

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");
        builder.Property(x => x.Name).HasMaxLength(DefaultStringLength);
        builder.Property(x => x.Surname).HasMaxLength(DefaultStringLength);
        builder.Property(x => x.Tckn).HasMaxLength(TcknLength);
        builder.Property(x => x.PhoneNumber).HasMaxLength(PhoneLength);
        builder.Property(x => x.UserName).HasMaxLength(_191_);
        builder.Property(x => x.NormalizedUserName).HasMaxLength(_191_);
        builder.Property(x => x.CreatedBy).HasMaxLength(EmailLength);
        builder.Property(x => x.UpdatedBy).HasMaxLength(EmailLength);
    }
}