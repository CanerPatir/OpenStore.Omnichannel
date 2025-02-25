namespace OpenStore.Omnichannel.Domain.LookupContext;

public abstract class LookupEntity : AuditableEntity
{
    public string Title { get; set; }
    public string DisplayTitle { get; set; }
    public string Description { get; set; }
}