namespace OpenStore.Omnichannel.Shared.Dto.Inventory;

public record InventoryListItemDto(Guid ProductId, string ProductTitle, string ProductPhotoUrl, string ProductSku, bool ContinueSellingWhenOutOfStock, int AvailableQuantity);