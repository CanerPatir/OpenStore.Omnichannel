namespace OpenStore.Omnichannel.Shared.Command.OrderContext;

public record Fulfill(Guid Id, string TrackingNumber, string CarrierIdentifier, IDictionary<Guid, int> LineItemQuantities) : ICommand<Guid>;