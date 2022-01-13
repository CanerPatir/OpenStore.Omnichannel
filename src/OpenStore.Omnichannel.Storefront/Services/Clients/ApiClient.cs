namespace OpenStore.Omnichannel.Storefront.Services.Clients;

public interface IApiClient
{
    CatalogClient Catalog { get; }
    CheckoutClient Checkout { get; }
    UserClient User { get; }
}

public class ApiClient : IApiClient
{
    internal const string ApiClientKey = "OpenStoreApiClient";

    public ApiClient(IHttpClientFactory clientFactory)
    {
        var httpClient = clientFactory.CreateClient(ApiClientKey);
        Catalog = new CatalogClient(httpClient);
        Checkout = new CheckoutClient(httpClient);
        User = new UserClient(httpClient);
    }

    public CatalogClient Catalog { get; }
    public CheckoutClient Checkout { get; }
    public UserClient User { get; }
}