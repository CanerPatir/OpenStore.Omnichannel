namespace OpenStore.Omnichannel.Domain.OrderContext;

public enum FinancialStatus
{
    Pending,
    Paid,
    Voided,
    Refunded,
    PartiallyRefunded,
    InvoiceSent
}