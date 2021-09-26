using System;

// ReSharper disable once CheckNamespace
namespace OpenStore.Omnichannel.Domain.InventoryContext
{
    public record InventoryCreated(Guid InventoryId, Guid VariantId, int Quantity, bool ContinueSellingWhenOutOfStock) : DomainEventBase(InventoryId);

    public record InventoryChanged(Guid InventoryId, int Quantity, int AvailableQuantity) : DomainEventBase(InventoryId);

    public record InventoryReserved(Guid InventoryId, int Quantity, int AvailableQuantity) : DomainEventBase(InventoryId);

    public record InventoryReleased(Guid InventoryId, int Quantity, int AvailableQuantity) : DomainEventBase(InventoryId);
}