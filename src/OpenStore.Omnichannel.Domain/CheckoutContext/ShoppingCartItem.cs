using System.Text.Json.Serialization;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.CheckoutContext;

public class ShoppingCartItem
{
    [JsonConstructor]
    public ShoppingCartItem(Guid id, Guid variantId, int quantity)
    {
        if (quantity < 1) throw new DomainException(Msg.Domain.Checkout.ShoppingCartItemQuantityShouldBeGreaterOrEqualThenZero);

        Id = id;
        VariantId = variantId;
        Quantity = quantity;
    }

    public static ShoppingCartItem Create(Guid variantId, int quantity)
    {
        return new ShoppingCartItem(Guid.NewGuid(), variantId, quantity);
    }

    public Guid Id { get; }
    public Guid VariantId { get; }
    public int Quantity { get; private set; }

    public void ChangeQuantity(int quantity)
    {
        if (quantity < 1) throw new DomainException(Msg.Domain.Checkout.ShoppingCartItemQuantityShouldBeGreaterOrEqualThenZero);
        Quantity = quantity;
    }
}