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

        protected ProductMedia(string host, string path, string type, string extension, string filename)
            : base(host, path, type, extension, filename)
        {
            Id = Guid.NewGuid();
        }

        public static ProductMedia Create(string host, string path, string type, string extension, string filename, int position, long? size, string title)
        {
            var productMedia = new ProductMedia(host, path, type, extension, filename)
            {
                Position = position,
                Size = size,
                Title = title
            };

            productMedia.ApplyChange(new ProductMediaCreated(
                productMedia.Id,
                productMedia.Host,
                productMedia.Path,
                productMedia.Type,
                productMedia.Extension,
                productMedia.Filename,
                productMedia.Title,
                productMedia.Position,
                productMedia.Size
            ));
            return productMedia;
        }
    }
}