namespace OpenStore.Omnichannel.Domain.OrderContext;

public class Refund
{
    public Guid OrderLineItemId { get; set; }
    public int Quantity { get; set; }
    public PriceInfo SubTotal { get; set; }
    public PriceInfo Tax { get; set; }
}