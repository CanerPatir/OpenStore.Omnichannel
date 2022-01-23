namespace OpenStore.Omnichannel.Domain.OrderContext;

public class Shipping
{
    public string Code { get; set; }
    public string Title { get; set; }
    public string Source { get; set; }
    public PriceInfo Price { get; set; }
    public List<TaxLine> TaxLines { get; set; }
    public string CarrierIdentifier { get; set; }
    public PriceInfo DiscountedPrice { get; set; }
}