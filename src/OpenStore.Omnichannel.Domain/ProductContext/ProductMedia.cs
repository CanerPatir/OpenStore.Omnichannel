using System;
using System.Collections.Generic;

// ReSharper disable CollectionNeverUpdated.Local

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class ProductMedia : MediaEntity
    {
        private readonly HashSet<Guid> _variantIds = new();

        public Guid? ProductId { get; set; }

        public IReadOnlyCollection<Guid> VariantIds => _variantIds;

        public ProductMedia(string host, string path, string type, string extension, string filename)
            : base(host, path, type, extension, filename)
        {
        }
    }
}