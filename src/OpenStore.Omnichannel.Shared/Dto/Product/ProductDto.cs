using System;
using System.Collections.Generic;

namespace OpenStore.Omnichannel.Shared.Dto.Product
{

    public class ProductDto 
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string Slug { get; set; } 
        public string MetaTitle { get; set; } 
        public string MetaDescription { get; set; }
        public ProductStatus Status { get; set; }
        
        public IEnumerable<ProductVariantDto> Variants { get; set; }
        public IEnumerable<ProductSaleChannelDto> Channels { get; set; }
        
        public ProductPricingDto Pricing { get; set; }
        public ProductInventoryDto Inventory { get; set; }
        public ProductShippingInfoDto ProductShippingInfoDto { get; set; }
        public IEnumerable<ProductMediaDto> Medias { get; set; }

        public IEnumerable<ProductVarianterAttributeDto> VarianterAttributes { get; set; }
    }
    
    public class ProductVarianterAttributeDto
    {
        public Guid Id { get; set; }
        public string  Name { get; set; }
        public IEnumerable<ProductVarianterAttributeValueDto> Values { get; set; }
    }

    public class ProductVarianterAttributeValueDto
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}