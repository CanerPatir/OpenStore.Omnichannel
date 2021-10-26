namespace OpenStore.Omnichannel.Domain.ChannelContext;

public class SaleChannel : AuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}