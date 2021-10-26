using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.LocalizationContext;

public class LocalizationSet : Entity<Guid>
{
    public virtual ICollection<Localization> Localizations { get; set; }
}