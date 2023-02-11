using System.Net;
using System.Net.Http.Json;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.Shared.ApiClient.Storefront;

public class CheckoutClient : BaseClient
{
    public CheckoutClient(HttpClient httpClient) : base(httpClient)
    {
    }


    protected override string Path => "api-sf/checkout";
    private string ShoppingCartPath => $"{Path}/shopping-cart";
    private string OrderSummaryPath => $"{Path}/order-summary";

    #region Shopping Cart

    public async Task<Guid> CreateShoppingCart(Guid? userId, CancellationToken cancellationToken = default)
    {
        var path = ShoppingCartPath;
        if (userId.HasValue)
        {
            path += $"?userId={userId}";
        }

        var resp = await HttpClient.PostAsync(path, null, cancellationToken);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<Guid>(cancellationToken: cancellationToken);
    }

    public async Task<Guid> AddItemToCart(Guid cartId, Guid variantId, int quantity, CancellationToken cancellationToken = default)
    {
        var resp = await HttpClient.PostAsync(Combine(ShoppingCartPath, $"{cartId}/items?variantId={variantId}&quantity={quantity}"), null, cancellationToken);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<Guid>(cancellationToken: cancellationToken);
    }

    public async Task RemoveItemFromCart(Guid cartId, Guid itemId, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.DeleteAsync(Combine(ShoppingCartPath, $"{cartId}/items/{itemId}"), cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task ChangeItemQuantityOfCart(Guid cartId, Guid itemId, int quantity, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.PostAsync(Combine(ShoppingCartPath, $"{cartId}/items/{itemId}?quantity={quantity}"), null, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<ShoppingCartQueryResult> GetCart(Guid cartId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await HttpClient.GetFromJsonAsync<ShoppingCartQueryResult>(Combine(ShoppingCartPath, $"{cartId}"), cancellationToken);
        }
        catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    #endregion
}