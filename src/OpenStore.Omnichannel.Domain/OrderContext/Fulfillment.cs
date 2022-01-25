using OpenStore.Domain;

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
    public FulfillmentStatus Status { get; protected set; }
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
            Status = FulfillmentStatus.Fulfilled,
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
        Status = FulfillmentStatus.TrackingInfoAdded;
    }

    public void Cancel()
    {
        Status = FulfillmentStatus.Unfulfilled;
        ResetTrackingInfo();
    }

    public void Hold()
    {
        Status = FulfillmentStatus.OnHold;
        ResetTrackingInfo();
    }

    public void Release()
    {
        Status = FulfillmentStatus.Unfulfilled;
        ResetTrackingInfo();
    }

    private void ResetTrackingInfo()
    {
        TrackingNumber = null;
        CarrierIdentifier = null;
    }
}