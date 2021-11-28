namespace OpenStore.Omnichannel.Storefront.Services.Clients;

public class ApiClient : IApiClient
{
    internal const string ApiClientKey = "OpenStoreApiClient";

    public ApiClient(IHttpClientFactory clientFactory)
    {
        var httpClient = clientFactory.CreateClient(ApiClientKey);
        Catalog = new CatalogStore(httpClient);
        ShoppingCart = new ShoppingCartStore(httpClient);
    }

    public CatalogStore Catalog { get; }
    public ShoppingCartStore ShoppingCart { get; }
}