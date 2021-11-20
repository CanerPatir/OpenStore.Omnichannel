using System.Text.Json.Serialization;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.CheckoutContext;

public class ShoppingCartItem
{
    [JsonConstructor]
    protected ShoppingCartItem(Guid id, Guid variantId, int quantity)
    {
        Id = id;
        VariantId = variantId;
        Quantity = quantity;
    }

    public static ShoppingCartItem Create(Guid variantId, int quantity)
    {
        if (quantity < 1) throw new DomainException(Msg.Domain.Checkout.ShoppingCartItemQuantityShouldBeGreaterOrEqualThenZero);

        return new ShoppingCartItem(Guid.NewGuid(), variantId, quantity);
    }

    public Guid Id { get; protected set; }
    public Guid VariantId { get; protected set; }
    public int Quantity { get; protected set; }

    public void ChangeQuantity(int quantity)
    {
        if (quantity < 1) throw new DomainException(Msg.Domain.Checkout.ShoppingCartItemQuantityShouldBeGreaterOrEqualThenZero);
    }
    
}