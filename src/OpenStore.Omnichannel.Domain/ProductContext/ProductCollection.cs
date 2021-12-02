using OpenStore.Domain;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

namespace OpenStore.Omnichannel.Domain.ProductContext;

public class ProductCollection : AuditableEntity
{
    private readonly HashSet<ProductCollectionItem> _productItems = new();

    public string Name { get; protected set; }
    public string Description { get; protected set; }

    public virtual IReadOnlyCollection<ProductCollectionItem> ProductItems => _productItems;

    protected ProductCollection()
    {
    }

    public static ProductCollection Create(CreateProductCollection command)
    {
        var (name, description) = command;
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(Msg.Domain.ProductCollection.CollectionNameIsRequired);
        }

        var productCollection = new ProductCollection
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description
        };
        productCollection.ApplyChange(new ProductCollectionCreated(productCollection.Id, name, description));
        return productCollection;
    }

    public void Update(UpdateProductCollection command)
    {
        var (_, name, description) = command;
        Name = name;
        Description = description;
        
        ApplyChange(new ProductCollectionUpdated(Id, name, description));
    }

    public void AddProduct(AddProductToCollection command)
    {
        var (_, productId) = command;
        if (_productItems.Any(x =>x.ProductId == productId))
        {
            return;
        }

        _productItems.Add(ProductCollectionItem.Create(Id, productId));
        
        ApplyChange(new ProductAddedToCollection(Id, productId));
    }
}