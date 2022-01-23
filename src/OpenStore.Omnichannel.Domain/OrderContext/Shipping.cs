namespace OpenStore.Omnichannel.Domain.OrderContext;

public class Shipping
{
    public string Title { get; set; }
    public string Source { get; set; }
    public PriceInfo Price { get; set; }
    public List<TaxLine> TaxLines { get; set; }
    public PriceInfo DiscountedPrice { get; set; }
    public string TrackingNumber { get; set; }
    public string TrackingCompany { get; set; }
}