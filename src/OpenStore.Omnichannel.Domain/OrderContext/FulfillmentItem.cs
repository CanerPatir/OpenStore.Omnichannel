// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace OpenStore.Omnichannel.Domain.OrderContext;

public class FulfillmentItem : AuditableEntity
{
    public Guid FulfillmentId { get; protected set; }
    public virtual Fulfillment Fulfillment { get; protected set; }

    public Guid OrderLineItemId { get; protected set; }
    public virtual OrderLineItem OrderLineItem { get; protected set; }

    public int Quantity { get; protected set; }

    protected FulfillmentItem()
    {
    }

    public static FulfillmentItem Create(Guid fulfillmentId, Guid orderLineItemId, int quantity)
    {
        return new()
        {
            FulfillmentId = fulfillmentId,
            OrderLineItemId = orderLineItemId,
            Quantity = quantity
        };
    }
}