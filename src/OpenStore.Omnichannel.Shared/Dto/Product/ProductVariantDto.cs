using System.Collections.Generic;
using OpenStore.Omnichannel.Shared.Dto.Media;

namespace OpenStore.Omnichannel.Shared.Dto.Product
{
    public class ProductVariantDto
    {
        // Pricing
        public decimal Price { get; set; } = 0;
        public decimal? CompareAtPrice { get; set; }
        public decimal? Cost { get; set; }

        // Inventory
        public string Sku { get; protected set; }
        public string Barcode { get; protected set; }
        public bool TrackQuantity { get; set; }
        public bool ContinueSellingWhenOutOfStock { get; set; }
        public uint Quantity { get; set; }

        public IEnumerable<MediaDto> Medias { get; set; }
        public IEnumerable<VariantAttributeValueDto> VariantAttributeValues { get; set; }
    }
}