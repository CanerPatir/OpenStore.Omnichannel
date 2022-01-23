namespace OpenStore.Omnichannel.Domain.OrderContext;

public class Fulfillment
{
    public FulfillmentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string TrackingNumber { get; set; }
    public string TrackingCompany { get; set; }
}