namespace OpenStore.Omnichannel.Domain.OrderContext;

public class PaymentSchedule
{
    public int Amount { get; set; }
    public DateTime DueAt { get; set; }
    public string Currency { get; set; }
    public DateTime IssuedAt { get; set; }
    public string CompletedAt { get; set; }
    public string ExpectedPaymentMethod { get; set; }
}