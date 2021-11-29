namespace OpenStore.Omnichannel.Storefront.Services.Clients;

public interface IApiClient
{
    CatalogClient Catalog { get; }
    ShoppingCartClient ShoppingCart { get; }
}

public class ApiClient : IApiClient
{
    internal const string ApiClientKey = "OpenStoreApiClient";

    public ApiClient(IHttpClientFactory clientFactory)
    {
        var httpClient = clientFactory.CreateClient(ApiClientKey);
        Catalog = new CatalogClient(httpClient);
        ShoppingCart = new ShoppingCartClient(httpClient);
    }

    public CatalogClient Catalog { get; }
    public ShoppingCartClient ShoppingCart { get; }
}