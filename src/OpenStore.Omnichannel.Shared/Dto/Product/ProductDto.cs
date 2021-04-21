using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenStore.Omnichannel.Shared.Dto.Product
{
    public class ProductDto
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = Msg.Validation.Required)]
        public string Title { get; set; }

        [Required(ErrorMessage = Msg.Validation.Required)]
        public string Description { get; set; }

        [Required(ErrorMessage = Msg.Validation.Required)]
        public string Handle { get; set; } // unique  

        public string Tags { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Draft;

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
        public string Name { get; set; }
        public IEnumerable<ProductVarianterAttributeValueDto> Values { get; set; }
    }

    public class ProductVarianterAttributeValueDto
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}