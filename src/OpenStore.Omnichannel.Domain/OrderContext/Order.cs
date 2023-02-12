using System.ComponentModel.DataAnnotations.Schema;
using OpenStore.Domain;
using OpenStore.Omnichannel.Shared.Command.OrderContext;
using OpenStore.Omnichannel.Shared.DomainEvents.OrderContext;
using OpenStore.Omnichannel.Shared.Enums;

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
    public OrderFinancialStatus FinancialStatus { get; protected set; }
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

    #region Flags

    [NotMapped] public bool IsPaid => false;
    [NotMapped] public bool IsFulfilled => Fulfillments.All(x => x.Status == OrderFulfillmentStatus.Fulfilled);
    [NotMapped] public bool IsPartiallyFulfilled => !IsFulfilled && Fulfillments.Any(x => x.Status == OrderFulfillmentStatus.Fulfilled);
    [NotMapped] public bool IsReturnInProgress => Returns.Any(x => x.Status == OrderReturnStatus.InProgress);
    [NotMapped] public bool IsReturned => Returns.All(x => x.Status == OrderReturnStatus.Returned);
    [NotMapped] public bool IsPartiallyReturned => !IsReturned && Returns.Any(x => x.Status == OrderReturnStatus.Returned) ;

    #endregion
    
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

    public static Order CreatePreorder(CreatePreorder command)
    {
        var order = new Order()
        {
            Id = new Guid(),
            // Customer = new CustomerInfo(),
            // ClientDetails = new ClientInfo(),
            // BillingAddress = new AddressInfo(),
            // ShippingAddress = new AddressInfo(),
            FinancialStatus = OrderFinancialStatus.Pending,
            IsPreorder = true
        };

        foreach (var item in command.LineItems)
        {
            order._lineItems.Add(
                OrderLineItem.Create(order
                    , item.ProductId
                    , item.VariantId
                    , item.Barcode
                    , item.Sku
                    , item.Title
                    , item.PhotoUrl
                    , item.Brand
                    , item.Price
                    , item.CurrencyCode
                    , item.Quantity
                    , item.Taxable
                    , item.Tax
                    , item.TaxCurrencyCode
                    , item.RequiresShipping
                )
            );
        }

        if (order.LineItems.IsNotEmpty())
        {
            order.TotalPrice = new PriceInfo(order.LineItems.Sum(x => x.Price.Amount), order.LineItems.First().Price.CurrencyCode);
            order.TotalTax = new PriceInfo(order.LineItems.Sum(x => x.Tax.Amount), order.LineItems.First().Tax.CurrencyCode);
        }

        order.ApplyChange(new PreorderCreated(order.Id));
        order.AppendTimelineItems(TimelineItemType.Log);

        return order;
    }

    public Guid Fulfill(FulfillOrder command)
    {
        // lineItems, quantities
        var fulfillment = Fulfillment.Create(command.TrackingNumber, command.CarrierIdentifier, command.LineItemQuantities);
        _fulfillments.Add(fulfillment);
        ApplyChange(new FulfillmentCreated(Id, fulfillment.Id, command.LineItemQuantities, command.TrackingNumber, command.CarrierIdentifier));
        AppendTimelineItems(TimelineItemType.Log);

        return fulfillment.Id;
    }

    public void Refund()
    {
        AppendTimelineItems(TimelineItemType.Log);
    }

    public void ReturnItems()
    {
        AppendTimelineItems(TimelineItemType.Log);
    }

    public void UpdateMasterData()
    {
        AppendTimelineItems(TimelineItemType.Log);
    }

    public void AddComment(string comment)
    {
        AppendTimelineItems(TimelineItemType.Comment, comment);
    }

    private void AppendTimelineItems(TimelineItemType type, string description = OrderTimelineDescriptions.Empty)
    {
        _timelineItems.Add(new TimelineItem(DateTime.UtcNow, description, type));
        // TODO: 
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