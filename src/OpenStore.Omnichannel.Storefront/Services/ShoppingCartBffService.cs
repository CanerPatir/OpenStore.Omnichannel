using System.Security.Claims;
using OpenIddict.Abstractions;
using OpenStore.Omnichannel.Storefront.Models.ShoppingCart;
using OpenStore.Omnichannel.Storefront.Services.Clients;

namespace OpenStore.Omnichannel.Storefront.Services;

public class ShoppingCartBffService : IBffService
{
    private const string CartCookieKey = "CartCookieKey";
    private readonly IApiClient _apiClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ShoppingCartBffService(IApiClient apiClient, IHttpContextAccessor httpContextAccessor)
    {
        _apiClient = apiClient;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext HttpContext => _httpContextAccessor.HttpContext;
    
    private ClaimsPrincipal User => HttpContext?.User;
    
    public async Task CreateShoppingCartIfNotExists(CancellationToken cancellationToken = default)
    {
        if (TryGetCartId(out var cartId))
        {
            var cartExists = await _apiClient.ShoppingCart.CheckCartExists(cartId, cancellationToken);
            if (cartExists)
            {
                return;
            }
        }

        cartId = await _apiClient.ShoppingCart.CreateShoppingCart(GetUserId(), cancellationToken);
        SetCartId(cartId);
    }

    public Task<Guid> AddItemToCart(Guid variantId, int quantity, CancellationToken cancellationToken = default)
    {
        var cartId = GetCartId();

        return _apiClient.ShoppingCart.AddItemToCart(cartId, variantId, quantity, cancellationToken);
    }

    public Task RemoveItemFromCart(Guid itemId, CancellationToken cancellationToken = default)
    {
        var cartId = GetCartId();

        return _apiClient.ShoppingCart.RemoveItemFromCart(cartId, itemId, cancellationToken);
    }

    public Task ChangeItemQuantityOfCart(Guid itemId, int quantity, CancellationToken cancellationToken = default)
    {
        var cartId = GetCartId();
        return _apiClient.ShoppingCart.ChangeItemQuantityOfCart(cartId, itemId, quantity, cancellationToken);
    }

    private async Task BindCartToUser(CancellationToken cancellationToken = default)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return;
        }     
        var cartId = GetCartId();
        await _apiClient.ShoppingCart.BindCartToUser(cartId, userId.Value, cancellationToken);
    }

    public async Task<ShoppingCartViewModel> GetShoppingCartViewModel(CancellationToken cancellationToken = default)
    {
        if (!TryGetCartId(out var cartId))
        {
            return null;
        }

        var shoppingCart = await _apiClient.ShoppingCart.GetCart(cartId, cancellationToken);
        if (shoppingCart is null)
        {
            return null;
        }
        
        return new ShoppingCartViewModel(shoppingCart);
    }

    private Guid? GetUserId()
    {
        if (User?.Identity?.IsAuthenticated == false)
        {
            return default;
        }
        
        var idClaim = User?.Claims.FirstOrDefault(c => c.Type == OpenIddictConstants.Claims.Subject)?.Value;

        return Guid.TryParse(idClaim, out var id) ? id : default;
    }

    private bool TryGetCartId(out Guid cartId)
    {
        cartId = Guid.Empty;
        if (_httpContextAccessor.HttpContext is null)
        {
            return false;
        }

        if (!HttpContext.Request.Cookies.TryGetValue(CartCookieKey, out var sessionValue))
        {
            return false;
        }

        if (!Guid.TryParse(sessionValue, out var _cartId))
        {
            return false;
        }

        cartId = _cartId;
        return true;
    }

    private Guid GetCartId()
    {
        if (TryGetCartId(out var cartId))
        {
            return cartId;
        }

        throw new ApplicationException(Msg.Application.CartNotCreatedYet);
    }

    private void SetCartId(Guid cartId)
    {
        HttpContext.Response.Cookies.Append(CartCookieKey, cartId.ToString(), new CookieOptions
        {
            IsEssential = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });
    }
}