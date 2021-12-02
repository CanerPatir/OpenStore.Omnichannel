// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

// ReSharper disable NonReadonlyMemberInGetHashCode

namespace OpenStore.Omnichannel.Domain.ProductContext;

public class ProductCollectionItem : AuditableEntity
{
    public Guid ProductCollectionId { get; protected set; }
    public virtual ProductCollection ProductCollection { get; protected set; }

    public Guid ProductId { get; protected set; }
    public virtual Product Product { get; protected set; }

    protected ProductCollectionItem()
    {
    }

    public static ProductCollectionItem Create(ProductCollection productCollection, Product product)
    {
        return new ProductCollectionItem()
        {
            ProductCollection = productCollection,
            ProductCollectionId = productCollection.Id,
            Product = product,
            ProductId = product.Id
        };
    }

    public static ProductCollectionItem Create(Guid productCollectionId, Guid productId)
    {
        return new ProductCollectionItem()
        {
            ProductCollectionId = productCollectionId,
            ProductId = productId
        };
    }

    #region equality

    protected bool Equals(ProductCollectionItem other)
        => base.Equals(other) && ProductCollectionId.Equals(other.ProductCollectionId) && ProductId.Equals(other.ProductId);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((ProductCollectionItem)obj);
    }

    public override int GetHashCode()
        => HashCode.Combine(base.GetHashCode(), ProductCollectionId, ProductId);

    #endregion
}