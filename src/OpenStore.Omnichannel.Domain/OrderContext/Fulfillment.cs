using OpenStore.Domain;
using OpenStore.Omnichannel.Shared.Enums;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CollectionNeverUpdated.Local
// ReSharper disable ReturnTypeCanBeEnumerable.Global
// ReSharper disable MemberCanBePrivate.Global

namespace OpenStore.Omnichannel.Domain.OrderContext;

public class Fulfillment : AuditableEntity
{
    private readonly HashSet<FulfillmentItem> _fulfillmentItems = new();

    public Guid OrderId { get; protected set; }
    public virtual Order Order { get; protected set; }

    public AddressInfo ShippingAddress { get; protected set; }
    public OrderFulfillmentStatus Status { get; protected set; }
    public bool ShippingRequired { get; set; }
    public string TrackingNumber { get; protected set; }
    public string CarrierIdentifier { get; protected set; }
    public virtual IReadOnlyCollection<FulfillmentItem> FulfillmentItems => _fulfillmentItems;
    
    public static Fulfillment Create(string trackingNumber, string carrierIdentifier, IDictionary<Guid, int> lineItemQuantities)
    {
        var fulfillment = new Fulfillment()
        {
            Id = Guid.NewGuid(),
            TrackingNumber = trackingNumber,
            CarrierIdentifier = carrierIdentifier,
            Status = OrderFulfillmentStatus.Fulfilled,
        };

        foreach (var (lineItemId, quantity) in lineItemQuantities)
        {
            fulfillment._fulfillmentItems.Add(FulfillmentItem.Create(fulfillment.Id, lineItemId, quantity));
        }

        return fulfillment;
    }

    public void AddTracking(string trackingNumber, string carrierIdentifier)
    {
        if (TrackingNumber is not null && CarrierIdentifier is not null)
        {
            throw new DomainException(Msg.Domain.Order.TrackingInfoAlreadyExits);
        }

        TrackingNumber = trackingNumber;
        CarrierIdentifier = carrierIdentifier;
        Status = OrderFulfillmentStatus.TrackingInfoAdded;
    }

    public void Cancel()
    {
        Status = OrderFulfillmentStatus.Unfulfilled;
        ResetTrackingInfo();
    }

    public void Hold()
    {
        Status = OrderFulfillmentStatus.OnHold;
        ResetTrackingInfo();
    }

    public void Release()
    {
        Status = OrderFulfillmentStatus.Unfulfilled;
        ResetTrackingInfo();
    }

    private void ResetTrackingInfo()
    {
        TrackingNumber = null;
        CarrierIdentifier = null;
    }
}