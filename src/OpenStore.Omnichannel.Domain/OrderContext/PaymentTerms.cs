namespace OpenStore.Omnichannel.Domain.OrderContext;

public class PaymentTerms
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public int DueInDays { get; set; }
    public List<PaymentSchedule> PaymentSchedules { get; set; }
}