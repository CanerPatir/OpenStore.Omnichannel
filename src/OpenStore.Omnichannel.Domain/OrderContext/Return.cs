using System.ComponentModel.DataAnnotations.Schema;

namespace OpenStore.Omnichannel.Domain.OrderContext;

public class Return : AuditableEntity
{
    public Guid OrderId { get; protected set; }
    public virtual Order Order { get; protected set; }
    
    public ReturnStatus Status { get; protected set; }
    public string Reason { get; protected set; }
    public virtual List<ReturnItem> ReturnItems { get; protected set; }
    
    [NotMapped] public decimal Subtotal => ReturnItems.Sum(x => x.PriceTotal);
    [NotMapped] public decimal TaxSubtotal => ReturnItems.Sum(x => x.TaxTotal);
    [NotMapped] public IEnumerable<TaxLine> TaxLinesSubtotal => ReturnItems.SelectMany(x => x.TaxLines);

    public void Refund()
    {
        Status = ReturnStatus.Refunded;
    }
    
    public void MarkAsReturned()
    {
        Status = ReturnStatus.Returned;
    }
}