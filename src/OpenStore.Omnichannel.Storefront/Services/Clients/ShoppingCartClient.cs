using System.Net;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.Storefront.Services.Clients;

public class ShoppingCartClient : BaseClient
{
    public ShoppingCartClient(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api-sf/shoppingCart";

    public async Task<Guid> CreateShoppingCart(Guid? userId, CancellationToken cancellationToken = default)
    {
        var path = Path;
        if (userId.HasValue)
        {
            path += $"?userId={userId}";
        }

        var resp = await HttpClient.PostAsync(path, null, cancellationToken);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<Guid>(cancellationToken: cancellationToken);
    }

    public async Task<bool> CheckCartExists(Guid cartId, CancellationToken cancellationToken = default)
    {
        try
        {
            var shoppingCart = await GetCart(cartId, cancellationToken);
            return shoppingCart is not null;
        }
        catch (HttpRequestException e) when(e.StatusCode == HttpStatusCode.NotFound)
        {
            return false;
        }
    }

    public async Task<Guid> AddItemToCart(Guid cartId, Guid variantId, int quantity, CancellationToken cancellationToken = default)
    {
        var resp = await HttpClient.PostAsync($"{Path}/{cartId}/items?variantId={variantId}&quantity={quantity}", null, cancellationToken);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<Guid>(cancellationToken: cancellationToken);
    }

    public async Task RemoveItemFromCart(Guid cartId, Guid itemId, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.DeleteAsync($"{Path}/{cartId}/items/{itemId}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task ChangeItemQuantityOfCart(Guid cartId, Guid itemId, int quantity, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.PostAsync($"{Path}/{cartId}/items/{itemId}?quantity={quantity}", null, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task BindCartToUser(Guid cartId, Guid userId, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.PostAsync($"{Path}/{cartId}/bind-to-user/{userId}", null, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<ShoppingCartResult> GetCart(Guid cartId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await HttpClient.GetFromJsonAsync<ShoppingCartResult>($"{Path}/{cartId}", cancellationToken);
        }
        catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}