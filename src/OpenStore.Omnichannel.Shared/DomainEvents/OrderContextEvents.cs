// ReSharper disable CheckNamespace
namespace OpenStore.Omnichannel.Shared.DomainEvents.OrderContext;

public record PreorderCreated(Guid OrderId): DomainEventBase(OrderId);

public record FulfillmentCreated(
    Guid OrderId
    , Guid FulfillmentId
    , IDictionary<Guid, int> LineItemQuantities
    , string TrackingNumber
    , string CarrierIdentifier) : DomainEventBase(OrderId);