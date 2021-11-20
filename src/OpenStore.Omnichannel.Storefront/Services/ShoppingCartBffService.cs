using OpenStore.Omnichannel.Storefront.Services.Clients;

namespace OpenStore.Omnichannel.Storefront.Services;

public class ShoppingCartBffService : IBffService
{
    private readonly ShoppingCartStore _shoppingCartStore;

    public ShoppingCartBffService(ShoppingCartStore shoppingCartStore)
    {
        _shoppingCartStore = shoppingCartStore;
    }

    public Task<Guid> CreateShoppingCart(CancellationToken cancellationToken = default) 
        => _shoppingCartStore.CreateShoppingCart(cancellationToken);

    public Task<Guid> AddItemToCart(Guid cartId, Guid variantId, int quantity, CancellationToken cancellationToken = default) 
        => _shoppingCartStore.AddItemToCart(cartId, variantId, quantity, cancellationToken);

    public Task RemoveItemFromCart(Guid cartId, Guid itemId, CancellationToken cancellationToken = default)
        => _shoppingCartStore.RemoveItemFromCart(cartId, itemId, cancellationToken);

    public Task ChangeItemQuantityOfCart(Guid cartId, Guid itemId, int quantity, CancellationToken cancellationToken = default)
        => _shoppingCartStore.ChangeItemQuantityOfCart(cartId, itemId, quantity, cancellationToken);

    public Task BindCartToUser(Guid cartId, Guid userId, CancellationToken cancellationToken = default)
        => _shoppingCartStore.BindCartToUser(cartId, userId, cancellationToken);
}