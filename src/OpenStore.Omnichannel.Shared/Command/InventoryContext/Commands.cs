namespace OpenStore.Omnichannel.Shared.Command.InventoryContext;

public record SetInventoryQuantity(Guid Id, int Quantity) : ICommand;

public record AddInventoryQuantity(Guid Id, int Quantity) : ICommand;