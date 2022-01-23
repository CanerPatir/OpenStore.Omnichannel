namespace OpenStore.Omnichannel.Domain.OrderContext;

public enum OrderLineItemStatus
{
    Created,
    Paid,
    ReturnInProgress,
    Returned,
    Refund,
    PartiallyRefund,
    Shipped,
    Fulfilled
}