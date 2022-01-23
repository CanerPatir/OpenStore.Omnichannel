using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.OrderContext;

public class Order : AggregateRoot<Guid>, IAuditableEntity, ISoftDeleteEntity
{
    public string Number { get; protected set; }
    public PaymentStatus PaymentStatus { get; protected set; }
    public DateTime ProcessedAt { get; protected set; }
    public DateTime ClosedAt { get; protected set; }
    public DateTime? CancelledAt { get; protected set; }
    public string CancelReason { get; protected set; }
    public ClientInfo ClientDetails { get; protected set; }
    public CustomerInfo Customer { get; protected set; }
    public PaymentInfo PaymentInfo { get; protected set; }
    public PaymentTerms PaymentTerms { get; protected set; }
    public AddressInfo BillingAddress { get; protected set; }
    public AddressInfo ShippingAddress { get; protected set; }
    public PriceInfo TotalPrice { get; protected set; }
    public PriceInfo TotalShippingPrice { get; protected set; }
    public PriceInfo TotalTax { get; protected set; }
    public List<DiscountCode> DiscountCodes { get; protected set; }
    public List<OrderLineItem> LineItems { get; protected set; }
    public List<OrderHistoryItem> HistoryItems { get; protected set; }
    public string Notes { get; protected set; }

    #region auditable members

    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }

    #endregion

    #region soft delete members

    public bool SoftDeleted { get; set; }

    #endregion
}