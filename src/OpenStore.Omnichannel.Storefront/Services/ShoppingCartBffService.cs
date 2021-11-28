using System.Text;
using OpenStore.Omnichannel.Storefront.Models.ShoppingCart;
using OpenStore.Omnichannel.Storefront.Services.Clients;

namespace OpenStore.Omnichannel.Storefront.Services;

public class ShoppingCartBffService : IBffService
{
    private const string CartSessionKey = "CartSessionKey";
    private readonly IApiClient _apiClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ShoppingCartBffService(IApiClient apiClient, IHttpContextAccessor httpContextAccessor)
    {
        _apiClient = apiClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid> CreateShoppingCart(CancellationToken cancellationToken = default)
    {
        return await _apiClient.ShoppingCart.CreateShoppingCart(cancellationToken);
    }

    public Task<Guid> AddItemToCart(Guid cartId, Guid variantId, int quantity, CancellationToken cancellationToken = default)
    {
        return _apiClient.ShoppingCart.AddItemToCart(cartId, variantId, quantity, cancellationToken);
    }

    public Task RemoveItemFromCart(Guid cartId, Guid itemId, CancellationToken cancellationToken = default)
    {
        return _apiClient.ShoppingCart.RemoveItemFromCart(cartId, itemId, cancellationToken);
    }

    public Task ChangeItemQuantityOfCart(Guid cartId, Guid itemId, int quantity, CancellationToken cancellationToken = default)
    {
        return _apiClient.ShoppingCart.ChangeItemQuantityOfCart(cartId, itemId, quantity, cancellationToken);
    }

    public Task BindCartToUser(Guid cartId, Guid userId, CancellationToken cancellationToken = default)
    {
        return _apiClient.ShoppingCart.BindCartToUser(cartId, userId, cancellationToken);
    }
    
    public async Task<ShoppingCartViewModel> GetShoppingCartViewModel(CancellationToken cancellationToken = default)
    {
         
        // var shoppingCart = await _apiClient.ShoppingCart.GetCart(cartId, cancellationToken);
        // return new ShoppingCartViewModel(shoppingCart);

        return null;
    }

    private Guid? GetCartId()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return null;
        }

        if (!_httpContextAccessor.HttpContext.Session.TryGetValue(CartSessionKey, out var sessionValue))
        {
            return null;
        }
        
        if (!Guid.TryParse(Encoding.UTF8.GetString(sessionValue), out var cartId))
        {
            return null;
        }
        
        return cartId;
    }
}