using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CollectionNeverUpdated.Local
// ReSharper disable ReturnTypeCanBeEnumerable.Global

namespace OpenStore.Omnichannel.Domain.OrderContext;

public class ReturnItem : AuditableEntity
{
    public Guid ReturnId { get; protected set; }
    public virtual Return Return { get; protected set; }

    public Guid OrderLineItemId { get; protected set; }
    public virtual OrderLineItem OrderLineItem { get; protected set; }

    public int Quantity { get; protected set; }

    [NotMapped] public decimal PriceTotal => OrderLineItem.Price.Amount * Quantity;
    [NotMapped] public decimal TaxTotal => OrderLineItem.Tax.Amount * Quantity;
    [NotMapped] public IEnumerable<TaxLine> TaxLines => OrderLineItem.TaxLines;
}