using OpenStore.Domain;

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

    public void AddProduct(AddProductToCollection command)
    {
        var (_, productId) = command;
        if (_productItems.Any(x => x.ProductId == productId))
        {
            return;
        }

        _productItems.Add(ProductCollectionItem.Create(Id, productId));

        ApplyChange(new ProductAddedToCollection(Id, productId));
    }
}