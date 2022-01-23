namespace OpenStore.Omnichannel.Domain.OrderContext;

public enum PaymentStatus
{
    Paid,
    Voided,
    Pending,
    Refunded,
    PartiallyRefunded
}