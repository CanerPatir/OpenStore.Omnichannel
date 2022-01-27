using OpenStore.Domain;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ReturnTypeCanBeEnumerable.Global
// ReSharper disable CollectionNeverUpdated.Local
// ReSharper disable ClassNeverInstantiated.Global

namespace OpenStore.Omnichannel.Domain.OrderContext;

public class OrderLineItem : Entity<Guid>, IAuditableEntity
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
    public string Title { get; protected set; }
    public string Brand { get; protected set; }
    public bool Taxable { get; protected set; }
    public string PhotoUrl { get; protected set; }
    public int Quantity { get; protected set; }
    public bool RequiresShipping { get; protected set; }

    public PriceInfo Price { get; protected set; }
    public PriceInfo Tax { get; protected set; }

    public IReadOnlyCollection<TaxLine> TaxLines => _taxLines;
    public virtual IReadOnlyCollection<FulfillmentItem> FulfillmentItems => _fulfillmentItems;
    public virtual IReadOnlyCollection<ReturnItem> ReturnItems => _returnItems;

    private OrderLineItem()
    {
    }

    public static OrderLineItem Create(
        Order order
        , Guid productId
        , Guid variantId
        , string barcode
        , string sku
        , string title
        , string photoUrl
        , string brand
        , decimal price
        , CurrencyCode currencyCode
        , int quantity
        , bool taxable
        , decimal? tax
        , CurrencyCode? taxCurrencyCode
        , bool requiresShipping)
    {
        return new()
        {
            Order = order,
            OrderId = order.Id,
            Barcode = barcode,
            Sku = sku,
            Title = title,
            Brand = brand,
            Price = new PriceInfo(price, currencyCode),
            Quantity = quantity,
            Taxable = taxable,
            Tax = taxable ? new PriceInfo(tax.Value, taxCurrencyCode.Value) : null,
            ProductId = productId,
            VariantId = variantId,
            TaxLines = { },
            RequiresShipping = requiresShipping,
            PhotoUrl = photoUrl,
        };
    }

    #region auditable members

    DateTime IAuditableEntity.CreatedAt { get; set; }
    string IAuditableEntity.CreatedBy { get; set; }
    DateTime? IAuditableEntity.UpdatedAt { get; set; }
    string IAuditableEntity.UpdatedBy { get; set; }

    #endregion
}