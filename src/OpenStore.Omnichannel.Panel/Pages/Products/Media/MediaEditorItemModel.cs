using OpenStore.Omnichannel.Shared.Dto.Media;

 
namespace OpenStore.Omnichannel.Panel.Pages.Products
{
    public class MediaEditorItemModel
    {
        public MediaDto Dto { get; set; }
        public bool Dropping { get; set; }
        public bool Selected { get; set; }
    }
}