using System.Net;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.Storefront.Services.Clients;

public class ShoppingCartStore : HttpStore
{
    public ShoppingCartStore(HttpClient httpClient) : base(httpClient)
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
        return await resp.Content.ReadFromJsonAsync<Guid>(cancellationToken: cancellationToken);
     }

    public async Task<bool> CheckCartExists(Guid cartId, CancellationToken cancellationToken = default)
    {
        try
        {
            await GetCart(cartId, cancellationToken);
            return true;
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }

            throw;
        }
    }

    public async Task<Guid> AddItemToCart(Guid cartId, Guid variantId, int quantity, CancellationToken cancellationToken = default)
    {
        var resp = await HttpClient.PostAsync($"{Path}/{cartId}/items?variantId={variantId}&quantity={quantity}", null, cancellationToken);
        return await resp.Content.ReadFromJsonAsync<Guid>(cancellationToken: cancellationToken));
    }

    public Task RemoveItemFromCart(Guid cartId, Guid itemId, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{Path}/{cartId}/items/{itemId}", cancellationToken);

    public Task ChangeItemQuantityOfCart(Guid cartId, Guid itemId, int quantity, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync($"{Path}/{cartId}/items/{itemId}?quantity={quantity}", null, cancellationToken);

    public Task BindCartToUser(Guid cartId, Guid userId, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync($"{Path}/{cartId}/bind-to-user/{userId}", null, cancellationToken);

    public Task<ShoppingCartResult> GetCart(Guid cartId, CancellationToken cancellationToken = default) =>
        HttpClient.GetFromJsonAsync<ShoppingCartResult>($"{Path}/{cartId}", cancellationToken);
}