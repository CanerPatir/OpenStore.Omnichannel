using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OpenStore.Omnichannel.Shared.Dto.Management.Product;

public class ProductCollectionDto
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = Msg.Validation.Required)]
    [MaxLength(70, ErrorMessage = Msg.Validation.MaxLength)]
    public string Handle { get; set; }

    [Required(ErrorMessage = Msg.Validation.Required)]
    public string Title { get; set; }

    public string Description { get; set; }

    public string MetaTitle { get; set; }
    public string MetaDescription { get; set; }

    [JsonIgnore] [NotMapped] public bool IsEdit => Id.HasValue && Id != default;
    [JsonIgnore] [NotMapped] public bool IsCreate => !IsEdit;
}