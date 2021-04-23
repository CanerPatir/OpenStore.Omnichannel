using System;

namespace OpenStore.Omnichannel.Shared.Dto.Product
{
    public class ProductMediaDto
    {
        public Guid MediaId { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Extension { get; set; }
        public string Filename { get; set; }
        public bool IsShowcase { get; set; }
        public short Order { get; set; }
    }
}