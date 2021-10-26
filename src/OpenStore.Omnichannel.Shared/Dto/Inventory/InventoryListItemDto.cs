namespace OpenStore.Omnichannel.Shared.Dto.Inventory;

public record InventoryListItemDto(Guid ProductId,
    string ProductTitle,
    string ProductPhotoUrl,
    string Sku,
    string Option1,
    string Option2,
    string Option3,
    bool ContinueSellingWhenOutOfStock,
    int Quantity,
    int AvailableQuantity)
{
    public string GetVariantDescription()
    {
        var val = Option1;

        if (!string.IsNullOrWhiteSpace(Option2))
        {
            val += " / " + Option2;
        }
        
        if (!string.IsNullOrWhiteSpace(Option3))
        {
            val += " / " + Option3;
        }
        
        return val;
    }
}