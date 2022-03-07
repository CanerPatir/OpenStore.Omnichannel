// ReSharper disable CheckNamespace
namespace OpenStore.Omnichannel.Shared.DomainEvents.InventoryContext;

public record InventoryCreated(Guid InventoryId, Guid VariantId, int Quantity, bool ContinueSellingWhenOutOfStock) : DomainEventBase(InventoryId);

public record InventoryChanged(Guid InventoryId, int Quantity, int AvailableQuantity, bool ContinueSellingWhenOutOfStock) : DomainEventBase(InventoryId);

public record InventoryReserved(Guid InventoryId, int Quantity, int AvailableQuantity) : DomainEventBase(InventoryId);

public record InventoryReleased(Guid InventoryId, int Quantity, int AvailableQuantity) : DomainEventBase(InventoryId);