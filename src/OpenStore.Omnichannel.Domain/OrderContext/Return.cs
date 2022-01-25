// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CollectionNeverUpdated.Local
// ReSharper disable ReturnTypeCanBeEnumerable.Global

using System.ComponentModel.DataAnnotations.Schema;

namespace OpenStore.Omnichannel.Domain.OrderContext;

public class Return : AuditableEntity
{
    private readonly HashSet<ReturnItem> _returnItems = new();

    public Guid OrderId { get; protected set; }
    public virtual Order Order { get; protected set; }

    public ReturnStatus Status { get; protected set; }
    public ReturnReason Reason { get; protected set; }
    public string OtherReasonDescription { get; protected set; }

    public virtual IReadOnlyCollection<ReturnItem> ReturnItems => _returnItems;

    [NotMapped] public decimal Subtotal => ReturnItems.Sum(x => x.PriceTotal);
    [NotMapped] public decimal TaxSubtotal => ReturnItems.Sum(x => x.TaxTotal);
    [NotMapped] public IEnumerable<TaxLine> TaxLinesSubtotal => ReturnItems.SelectMany(x => x.TaxLines);

    public bool ShippingRequired { get; set; }
    public string TrackingNumber { get; protected set; }
    public string CarrierIdentifier { get; protected set; }
    
    private Return()
    {
    }

    public static Return Create(ReturnReason returnReason)
    {
        return new()
        {
            Status = ReturnStatus.InProgress,
            
        };
    }

    public void Refund()
    {
        Status = ReturnStatus.Refunded;
    }

    public void MarkAsReturned()
    {
        Status = ReturnStatus.Returned;
    }
}