using System;
using System.Collections.Generic;

namespace OpenStore.Omnichannel.Shared.Dto.Product
{
    public class ProductMediaDto
    {
        public Guid Id { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Extension { get; set; }
        public string Filename { get; set; }
        public int Position { get; set; }
        public long? Size { get; set; }

        public IEnumerable<Guid> VariantIds { get; set; } = new List<Guid>();
    }
}