namespace OpenStore.Omnichannel.Storefront.Services.Clients;

public class ShoppingCartStore : HttpStore
{
    public ShoppingCartStore(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api-sf/shoppingCart";

    public async Task<Guid> CreateShoppingCart(CancellationToken cancellationToken = default)
    {
        var resp = await HttpClient.PostAsync(Path, null, cancellationToken);
        return await resp.Content.ReadFromJsonAsync<Guid>(cancellationToken: cancellationToken);
    }

    public async Task<Guid> AddItemToCart(Guid cartId, Guid variantId, int quantity, CancellationToken cancellationToken = default)
    {
        var resp = await HttpClient.PostAsync($"{Path}/{cartId}/items?variantId={variantId}&quantity={quantity}", null, cancellationToken);
        return await resp.Content.ReadFromJsonAsync<Guid>(cancellationToken: cancellationToken);
    }

    public Task RemoveItemFromCart(Guid cartId, Guid itemId, CancellationToken cancellationToken = default)
        => HttpClient.DeleteAsync($"{Path}/{cartId}/items/{itemId}", cancellationToken);

    public Task ChangeItemQuantityOfCart(Guid cartId, Guid itemId, int quantity, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync($"{Path}/{cartId}/items/{itemId}?quantity={quantity}", null, cancellationToken);

    public Task BindCartToUser(Guid cartId, Guid userId, CancellationToken cancellationToken = default)
        => HttpClient.PostAsync($"{Path}/{cartId}/bind-to-user/{userId}", null, cancellationToken);
}