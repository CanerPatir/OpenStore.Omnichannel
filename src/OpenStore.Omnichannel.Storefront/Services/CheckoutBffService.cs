using System.Security.Claims;
using OpenIddict.Abstractions;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;
using OpenStore.Omnichannel.Storefront.Models.Checkout;
using OpenStore.Omnichannel.Storefront.Services.Clients;

namespace OpenStore.Omnichannel.Storefront.Services;

public class CheckoutBffService : IBffService
{
    private const string CartCookieKey = "CartCookieKey";
    private readonly IApiClient _apiClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CheckoutBffService(IApiClient apiClient, IHttpContextAccessor httpContextAccessor)
    {
        _apiClient = apiClient;
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext HttpContext => _httpContextAccessor.HttpContext;
    private IDictionary<object, object> HttpContextCache => HttpContext.Items;
    private ClaimsPrincipal User => HttpContext?.User;

    public async Task CreateShoppingCartIfNotExists(CancellationToken cancellationToken = default)
    {
        if (TryGetCartId(out var cartId))
        {
            var cartExists = await CheckCartExists(cartId, cancellationToken);
            if (cartExists)
            {
                return;
            }
        }

        cartId = await _apiClient.Checkout.CreateShoppingCart(GetUserId(), cancellationToken);
        SetCartId(cartId);
    }

    public Task<Guid> AddItemToCart(Guid variantId, int quantity, CancellationToken cancellationToken = default)
    {
        var cartId = GetCartId();

        return _apiClient.Checkout.AddItemToCart(cartId, variantId, quantity, cancellationToken);
    }

    public Task RemoveItemFromCart(Guid itemId, CancellationToken cancellationToken = default)
    {
        var cartId = GetCartId();
        return _apiClient.Checkout.RemoveItemFromCart(cartId, itemId, cancellationToken);
    }

    public Task ChangeItemQuantityOfCart(Guid itemId, int quantity, CancellationToken cancellationToken = default)
    {
        var cartId = GetCartId();
        return _apiClient.Checkout.ChangeItemQuantityOfCart(cartId, itemId, quantity, cancellationToken);
    }

    private async Task BindCartToUser(CancellationToken cancellationToken = default)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return;
        }

        var cartId = GetCartId();
        await _apiClient.Checkout.BindCartToUser(cartId, userId.Value, cancellationToken);
    }
    
    public async Task<ShoppingCartViewModel> GetShoppingCartViewModel(CancellationToken cancellationToken = default)
    {
        if (!TryGetCartId(out var cartId))
        {
            return null;
        }

        var shoppingCart = await GetCartInternal(cartId, cancellationToken);
        if (shoppingCart is null)
        {
            return null;
        }

        return new ShoppingCartViewModel(shoppingCart);
    }

    public async Task<FlyoutShoppingCartViewModel> GetFlyoutShoppingCartViewModel(CancellationToken cancellationToken = default)
    {
        var itemCount = 0;
        var shoppingCartViewModel = await GetShoppingCartViewModel(cancellationToken);
        if (shoppingCartViewModel is not null)
        {
            itemCount = shoppingCartViewModel.ShoppingCart.Items.Count;
        }

        return new FlyoutShoppingCartViewModel(itemCount);
    }
    
    public async Task<OrderSummaryViewModel> GetOrderSummary(CancellationToken cancellationToken)
    {
        var shoppingCartViewModel = await GetShoppingCartViewModel(cancellationToken);

        var total = shoppingCartViewModel?.ShoppingCart?.Items.Sum(x => x.Price * x.Quantity) ?? 0;
        return new OrderSummaryViewModel(total, null, null, total);
    }

    private async Task<bool> CheckCartExists(Guid cartId, CancellationToken cancellationToken = default)
    {
        var shoppingCart = await GetCartInternal(cartId, cancellationToken);
        return shoppingCart is not null;
    }

    private async Task<ShoppingCartResult> GetCartInternal(Guid cartId, CancellationToken cancellationToken)
    {
        // todo: remove this shit caching
        const string cacheKey = "shoppingCartViewModel";
        if (HttpContextCache.TryGetValue(cacheKey, out var cacheResult))
        {
            return (ShoppingCartResult)cacheResult;
        }
        
        var shoppingCartResult = await _apiClient.Checkout.GetCart(cartId, cancellationToken);
        if (shoppingCartResult is null)
        {
            return null;
        }

        HttpContextCache[cacheKey] = shoppingCartResult;
        
        return shoppingCartResult;
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