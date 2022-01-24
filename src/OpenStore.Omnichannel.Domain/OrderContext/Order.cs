using System.ComponentModel.DataAnnotations.Schema;
using OpenStore.Domain;

// ReSharper disable ReturnTypeCanBeEnumerable.Global
// ReSharper disable MemberCanBeProtected.Global

namespace OpenStore.Omnichannel.Domain.OrderContext;

public class Order : AggregateRoot<Guid>, IAuditableEntity, ISoftDeleteEntity
{
    private List<string> _tags = new();
    private List<Discount> _discounts = new();
    private List<TimelineItem> _timelineItems = new();
    private readonly HashSet<Return> _returns = new();
    private readonly HashSet<Fulfillment> _fulfillments = new();
    private readonly HashSet<OrderLineItem> _lineItems = new();

    public string Number { get; protected set; }
    public FinancialStatus FinancialStatus { get; protected set; }
    public DateTime ProcessedAt { get; protected set; }
    public DateTime ClosedAt { get; protected set; }
    public DateTime? CancelledAt { get; protected set; }
    public string CancelReason { get; protected set; }
    public bool IsCancelled { get; protected set; }
    public string Notes { get; protected set; }
    public ClientInfo ClientDetails { get; protected set; }
    public CustomerInfo Customer { get; protected set; }
    public Payment Payment { get; protected set; }
    public AddressInfo BillingAddress { get; protected set; }
    public AddressInfo ShippingAddress { get; protected set; }
    public PriceInfo TotalPrice { get; protected set; }
    public PriceInfo TotalShippingPrice { get; protected set; }
    public PriceInfo TotalTax { get; protected set; }

    public IReadOnlyCollection<string> Tags => _tags; // json 
    public IReadOnlyCollection<Discount> Discounts => _discounts; // json
    public IReadOnlyCollection<TimelineItem> TimelineItems => _timelineItems; // json

    public virtual IReadOnlyCollection<OrderLineItem> LineItems => _lineItems;
    public virtual IReadOnlyCollection<Return> Returns => _returns;
    public virtual IReadOnlyCollection<Fulfillment> Fulfillments => _fulfillments;

    [NotMapped] public bool IsPaid => false;
    [NotMapped] public bool IsFulfilled => false;
    [NotMapped] public bool IsPartiallyFulfilled => false;
    [NotMapped] public bool IsReturnInProgress => Returns.Any(x => x.Status == ReturnStatus.InProgress);
    [NotMapped] public bool IsReturned => Returns.All(x => x.Status == ReturnStatus.Returned);
    [NotMapped] public bool IsPartiallyReturned => false;
    [NotMapped] public bool Is => false;

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

/*
 public enum OrderStatus
{
    Created,
    Paid,
    Cancelled,
    ReturnInProgress,
    Returned,
    
    Refund,
    PartiallyRefund,
    
    PartiallyFulfilled,
    Fulfilled
}
 */