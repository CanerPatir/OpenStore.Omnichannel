using OpenStore.Omnichannel.Shared.Command;

namespace OpenStore.Omnichannel.Domain.InventoryContext;

public record SetInventoryQuantity(Guid Id, int Quantity) : ICommand;

public record AddInventoryQuantity(Guid Id, int Quantity) : ICommand;