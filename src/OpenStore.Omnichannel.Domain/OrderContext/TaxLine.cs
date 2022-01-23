namespace OpenStore.Omnichannel.Domain.OrderContext;

public class TaxLine
{
    public double Rate { get; set; }
    public string Title { get; set; }
    public PriceInfo Price { get; set; }
}