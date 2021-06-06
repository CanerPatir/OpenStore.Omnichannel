using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Panel.ViewModels
{
    public class MediaEditorItemModel
    {
        public ProductMediaDto Dto { get; set; }
        public bool Dropping { get; set; }
        public bool Selected { get; set; }
    }
}