using OpenStore.Domain;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

namespace OpenStore.Omnichannel.Domain.ProductContext;

public class ProductCollection : AuditableEntity
{
    private readonly HashSet<ProductCollectionItem> _productItems = new();

    public string Title { get; protected set; }
    public string Description { get; protected set; }
    public string Handle { get; protected set; }
    public string MetaTitle { get; protected set; }
    public string MetaDescription { get; protected set; }

    public virtual IReadOnlyCollection<ProductCollectionItem> ProductItems => _productItems;

    public virtual ProductCollectionMedia Media { get; protected set; }

    protected ProductCollection()
    {
    }

    public static ProductCollection Create(CreateProductCollection command)
    {
        var model = command.Model;
        if (string.IsNullOrWhiteSpace(model.Title))
        {
            throw new DomainException(Msg.Domain.ProductCollection.CollectionNameIsRequired);
        }

        if (string.IsNullOrWhiteSpace(model.Handle))
        {
            throw new DomainException(Msg.Domain.ProductCollection.CollectionHandleIsRequired);
        }

        var productCollection = new ProductCollection
        {
            Id = Guid.NewGuid(),
            Handle = model.Handle,
            Title = model.Title,
            Description = model.Description,
            MetaTitle = model.MetaTitle,
            MetaDescription = model.MetaDescription
        };
        productCollection.ApplyChange(new ProductCollectionCreated(productCollection.Id, model));
        return productCollection;
    }

    public void Update(UpdateProductCollection command)
    {
        var (_, model) = command;
        Handle = model.Handle;
        Title = model.Title;
        Description = model.Description;
        MetaTitle = model.MetaTitle;
        MetaDescription = model.MetaDescription;

        ApplyChange(new ProductCollectionUpdated(Id, model));
    }

    public void AddProduct(Guid productId)
    {
         if (_productItems.Any(x => x.ProductId == productId))
        {
            return;
        }

        _productItems.Add(ProductCollectionItem.Create(Id, productId));

        ApplyChange(new ProductAddedToCollection(Id, productId));
    }

    public ProductCollectionMediaDto ChangeImage(ChangeProductCollectionImage command, string host, string path)
    {
        var (_, (fileName, type, size, position, _)) = command;
        Media = ProductCollectionMedia.Create(host, path, type, Path.GetExtension(fileName), fileName, fileName, position, size);

        var productCollectionMediaDto = new ProductCollectionMediaDto(
            Media.Host,
            Media.Path,
            Media.Type,
            Media.Extension,
            Media.Filename,
            Media.Title,
            Media.Position,
            Media.Size
        );
        ApplyChange(new ProductCollectionImageChanged(Id, productCollectionMediaDto));
        
        return productCollectionMediaDto;
    }

    public void RemoveImage()
    {
        Media = null;
        ApplyChange(new ProductCollectionImageRemoved(Id));
    }

    public void RemoveItem(RemoveProductCollectionItem command)
    {
        _productItems.RemoveWhere(x => x.ProductId == command.ProductId);
        
        ApplyChange(new ProductCollectionItemRemoved(Id, command.ProductId));
    }
}