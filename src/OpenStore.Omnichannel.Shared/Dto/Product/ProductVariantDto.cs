using System;
using System.Collections.Generic;

namespace OpenStore.Omnichannel.Shared.Dto.Product
{
    public class ProductVariantDto 
    {
        public ProductPricingDto Pricing { get; set; }
        public ProductInventoryDto Inventory { get; set; }
        public ProductShippingInfoDto ProductShippingInfoDto { get; set; }
        public IEnumerable<ProductMediaDto> Medias { get; set; }
        
        public IEnumerable<ProductVariantAttributeValueDto> AttributeValues { get; set; }
    }

    public class ProductVariantAttributeValueDto
    {
        public Guid AttributeId { get; set; }
        public Guid AttributeValueId { get; set; }
    }
}