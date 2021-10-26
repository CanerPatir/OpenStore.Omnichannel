using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.LocalizationContext;

public class Localization : Entity<Guid>
{
    public Guid LocalizationSetId { get; set; }
    public virtual LocalizationSet LocalizationSet { get; set; }

    public string CultureCode { get; set; }
    public string Value { get; set; }
}