using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Panel.ViewModels.Products;

public class MediaEditorItemModel
{
    public ProductMediaDto Dto { get; set; }
    public bool Dropping { get; set; }
    public bool Selected { get; set; }
}