// ReSharper disable ReturnTypeCanBeEnumerable.Global
// ReSharper disable CollectionNeverUpdated.Local
// ReSharper disable ClassNeverInstantiated.Global

namespace OpenStore.Omnichannel.Domain.OrderContext;

public class OrderLineItem : AuditableEntity
{
    private List<TaxLine> _taxLines = new();
    private readonly HashSet<FulfillmentItem> _fulfillmentItems = new();
    private readonly HashSet<ReturnItem> _returnItems = new();

    public Guid OrderId { get; protected set; }
    public virtual Order Order { get; protected set; }

    public Guid ProductId { get; protected set; }
    public Guid VariantId { get; protected set; }
    public string Sku { get; protected set; }
    public string Barcode { get; protected set; }
    public string Name { get; protected set; }
    public int Grams { get; protected set; }
    public string Title { get; protected set; }
    public string Brand { get; protected set; }
    public bool Taxable { get; protected set; }
    public bool PhotoUrl { get; protected set; }
    public int Quantity { get; protected set; }
    public string VariantTitle { get; protected set; }
    public string TotalDiscount { get; protected set; }
    public bool RequiresShipping { get; protected set; }

    public PriceInfo Price { get; protected set; }
    public PriceInfo Tax { get; protected set; }

    public IReadOnlyCollection<TaxLine> TaxLines => _taxLines;
    public virtual IReadOnlyCollection<FulfillmentItem> FulfillmentItems => _fulfillmentItems;
    public virtual IReadOnlyCollection<ReturnItem> ReturnItems => _returnItems;
}