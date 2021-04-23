using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenStore.Omnichannel.Shared.Dto.Product
{
    public class ProductDto
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = Msg.Validation.Required)]
        [MaxLength(70, ErrorMessage = Msg.Validation.MaxLength)]
        public string Handle { get; set; } // unique  

        [Required(ErrorMessage = Msg.Validation.Required)]
        // [Range(20, 80, ErrorMessage = Msg.Validation.Range)]
        public string Title { get; set; }

        [Required(ErrorMessage = Msg.Validation.Required)]
        public string Description { get; set; }

        public ProductMetaDto Meta { get; set; } = new();

        public string Tags { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Draft;

        public IEnumerable<ProductVariantDto> Variants { get; set; }
        public IEnumerable<ProductSaleChannelDto> Channels { get; set; }

        public ProductPricingDto Pricing { get; set; } = new();
        public ProductInventoryDto Inventory { get; set; } = new();
        public ProductShippingInfoDto Shipping { get; set; } = new();
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