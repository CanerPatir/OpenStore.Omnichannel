namespace OpenStore.Omnichannel.Domain.OrderContext;

public class Fulfillment : AuditableEntity
{
    public Guid OrderId { get; protected set; }
    public virtual Order Order { get; protected set; }

    public virtual List<FulfillmentItem> FulfillmentItems { get; protected set; }

    public string TrackingNumber { get; protected set; }
    public string CarrierIdentifier { get; protected set; }
    public FulfillmentStatus Status { get; protected set; }
}