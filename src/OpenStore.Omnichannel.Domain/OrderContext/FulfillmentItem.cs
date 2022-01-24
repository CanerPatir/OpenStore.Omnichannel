namespace OpenStore.Omnichannel.Domain.OrderContext;

public class FulfillmentItem : AuditableEntity
{
    public Guid FulfillmentId { get; protected set; }
    public virtual Fulfillment Fulfillment { get; protected set; }

    public Guid OrderLineItemId { get; protected set; }
    public virtual OrderLineItem OrderLineItem { get; protected set; }

    public int Quantity { get; protected set; }
}