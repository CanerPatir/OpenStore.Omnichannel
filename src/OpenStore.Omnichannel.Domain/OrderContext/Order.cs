using System.ComponentModel.DataAnnotations.Schema;
using OpenStore.Domain;
using OpenStore.Omnichannel.Shared.Command.OrderContext;
using OpenStore.Omnichannel.Shared.DomainEvents.OrderContext;

// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Local
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ReturnTypeCanBeEnumerable.Global

namespace OpenStore.Omnichannel.Domain.OrderContext;

public class Order : AggregateRoot<Guid>, IAuditableEntity, ISoftDeleteEntity
{
    private List<string> _tags = new();
    private List<Discount> _discounts = new();
    private List<TimelineItem> _timelineItems = new();
    private readonly HashSet<Return> _returns = new();
    private readonly HashSet<Fulfillment> _fulfillments = new();
    private readonly HashSet<OrderLineItem> _lineItems = new();

    public int Number { get; protected set; }
    public FinancialStatus FinancialStatus { get; protected set; }
    public DateTime ProcessedAt { get; protected set; }
    public DateTime ClosedAt { get; protected set; }
    public DateTime? CancelledAt { get; protected set; }
    public string CancelReason { get; protected set; }
    public bool IsCancelled { get; protected set; }
    public bool IsPreorder { get; protected set; }
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

    #region auditable members

    DateTime IAuditableEntity.CreatedAt { get; set; }
    string IAuditableEntity.CreatedBy { get; set; }
    DateTime? IAuditableEntity.UpdatedAt { get; set; }
    string IAuditableEntity.UpdatedBy { get; set; }

    #endregion

    #region soft delete members

    public bool SoftDeleted { get; set; }

    #endregion

    private Order()
    {
    }

    public static Order CreatePreorder(CreatePreorderCommand command)
    {
        var order = new Order()
        {
            Id = new Guid(),
            Customer = new CustomerInfo(),
            ClientDetails = new ClientInfo(),

            BillingAddress = new AddressInfo(),
            ShippingAddress = new AddressInfo(),
            FinancialStatus = FinancialStatus.Pending,
            IsPreorder = true
        };

        foreach (var (productId, variantId, barcode, sku, title, photoUrl, brand, price, currencyCode, quantity, taxable, tax, taxCurrencyCode, requiresShipping) in
                 command.LineItems)
        {
            order._lineItems.Add(OrderLineItem.Create(order
                , productId
                , variantId
                , barcode
                , sku
                , title
                , photoUrl
                , brand
                , price
                , currencyCode
                , quantity
                , taxable
                , tax
                , taxCurrencyCode
                , requiresShipping
            ));
        }

        order.TotalPrice = new PriceInfo();
        order.TotalTax = new PriceInfo();

        order.ApplyChange(new OrderCreated(order.Id));
        order.AppendTimelineItems();

        return order;
    }

    public Guid Fulfill(Fulfill command)
    {
        // lineItems, quantities
        var (_, trackingNumber, carrierIdentifier, lineItemQuantities) = command;
        var fulfillment = Fulfillment.Create(trackingNumber, carrierIdentifier, lineItemQuantities);
        _fulfillments.Add(fulfillment);
        ApplyChange(new FulfillmentCreated(Id, fulfillment.Id, lineItemQuantities, trackingNumber, carrierIdentifier));
        AppendTimelineItems();

        return fulfillment.Id;
    }

    public void Refund()
    {
        AppendTimelineItems();
    }

    public void ReturnItems()
    {
        AppendTimelineItems();
    }

    public void UpdateMasterData()
    {
        AppendTimelineItems();
    }

    public void AddComment()
    {
        AppendTimelineItems();
    }

    private void AppendTimelineItems()
    {
    }
}

/*
public enum OrderStatus
{
    Preorder,
    Paid, // payment success but fulfillment empty
    Cancelled, // user cancel
    ReturnInProgress,
    Returned,
    
    Refund,
    PartiallyRefund,
    
    PartiallyFulfilled,
    Fulfilled
    
    OnHold , any fulfillment on hold
}
 */