namespace OpenStore.Omnichannel.Domain.OrderContext;

public class OrderLineItem : AuditableEntity
{
    public Guid ProductId { get; protected set; }
    public Guid VariantId { get; protected set; }

    public OrderLineItemStatus Status { get; protected set; }

    public string Sku { get; protected set; }
    public string Barcode { get; protected set; }
    public string Name { get; protected set; }
    public int Grams { get; protected set; }
    public string Title { get; protected set; }
    public string Brand { get; protected set; }
    public bool Taxable { get; protected set; }
    public int Quantity { get; protected set; }
    public string VariantTitle { get; protected set; }
    public string TotalDiscount { get; protected set; }
    public bool RequiresShipping { get; protected set; }
    public PriceInfo Price { get; protected set; }
    public Fulfillment Fulfillment { get; protected set; }
    public Refund Refund { get; protected set; }
    public Shipping Shipping { get; protected set; }
    public List<TaxLine> TaxLines { get; protected set; }
}