using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable CollectionNeverUpdated.Local

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class ProductMedia : MediaEntity
    {
        private readonly HashSet<Guid> _variantIds = new();

        public Guid? ProductId { get; set; }

        public IReadOnlyCollection<Guid> VariantIds => _variantIds;

        [NotMapped] public string Url => $"{Host?.TrimEnd('/')}/{Path}";

        protected ProductMedia()
        {
        }

        internal ProductMedia(Guid id)
        {
            Id = id;
        }

        public static ProductMedia Create(string host, string path, string type, string extension, string filename, int position, long? size, string title)
        {
            var productMedia = new ProductMedia
            {
                Id = Guid.NewGuid(),
                Host =  host,
                Path = path,
                Type = type,
                Extension = extension,
                Filename = filename,
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