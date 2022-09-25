namespace OpenStore.Omnichannel.Domain.OrderContext;

public enum OrderFinancialStatus
{
    Pending,
    Paid,
    Voided,
    Refunded,
    PartiallyRefunded,
    InvoiceSent
}