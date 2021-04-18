namespace OpenStore.Omnichannel.Shared.Dto.Product
{
    public class ProductInventoryDto
    {
        public bool TrackQuantity { get; set; }
        public bool ContinueSellingWhenOutOfStock { get; set; }
        public uint Quantity { get; set; }
    }
}