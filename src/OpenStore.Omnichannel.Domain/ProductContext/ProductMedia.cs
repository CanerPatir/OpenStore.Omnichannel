using OpenStore.Omnichannel.Shared.DomainEvents.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

// ReSharper disable CollectionNeverUpdated.Local

namespace OpenStore.Omnichannel.Domain.ProductContext;

public class ProductMedia : MediaEntity
{
    private readonly List<Guid> _variantIds = new();

    public Guid? ProductId { get; protected set; }

    public IReadOnlyCollection<Guid> VariantIds => _variantIds;

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
            Host = host,
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

    public void Update(ProductMediaDto productMediaDto)
    {
        Host = productMediaDto.Host;
        Path = productMediaDto.Path;
        Type = productMediaDto.Type;
        Extension = productMediaDto.Extension;
        Filename = productMediaDto.Filename;
        Title = productMediaDto.Title;
        Position = productMediaDto.Position;
        Size = productMediaDto.Size;
    }

    public void RemoveVariant(Variant variant) => RemoveVariant(variant.Id);
    
    public void RemoveVariant(Guid variantId)
    {
        if (_variantIds.All(x => x != variantId))
        {
            return;
        }

        _variantIds.Remove(variantId);
    }

    public void AddVariant(Variant variant) => AddVariant(variant.Id);

    public void AddVariant(Guid variantId)
    {
        if (_variantIds.Any(x => x == variantId))
        {
            return;
        }

        _variantIds.Add(variantId);
    }
}