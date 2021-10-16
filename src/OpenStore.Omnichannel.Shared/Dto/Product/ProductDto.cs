using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenStore.Omnichannel.Shared.Dto.Product;

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

    public ProductStatus Status { get; set; } = ProductStatus.Draft;
    public bool HasMultipleVariants { get; set; }

    // fulfillment
    public decimal? Weight { get; set; }
    public string WeightUnit { get; set; }
    public string HsCode { get; set; }
    public bool IsPhysicalProduct { get; set; } = true;

    // seo
    public string MetaTitle { get; set; }
    public string MetaDescription { get; set; }
    public string Tags { get; set; }

    public List<VariantDto> Variants { get; set; } = new()
    {
        new VariantDto()
    };

    public IEnumerable<ProductMediaDto> Medias { get; set; } = new List<ProductMediaDto>();
    public List<ProductOptionDto> Options { get; set; } = new();

    [NotMapped] public bool IsEdit => Id.HasValue && Id != default;
    [NotMapped] public bool IsCreate => !IsEdit;
}