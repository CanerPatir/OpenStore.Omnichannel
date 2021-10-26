using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Domain.LookupContext;

public class CategoryProduct
{
    public CategoryProduct(Guid categoryId, Guid productId)
    {
        CategoryId = categoryId;
        ProductId = productId;
    }

    public Guid CategoryId { get; protected set; }
    public virtual Category Category { get; protected set; }

    public Guid ProductId { get; protected set; }
    public virtual Product Product { get; protected set; }
}