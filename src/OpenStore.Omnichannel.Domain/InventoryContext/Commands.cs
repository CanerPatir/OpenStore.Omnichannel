using MediatR;

namespace OpenStore.Omnichannel.Domain.InventoryContext;

public record SetInventoryQuantity(Guid Id, int Quantity) : IRequest;

public record AddInventoryQuantity(Guid Id, int Quantity) : IRequest;