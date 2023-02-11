namespace OpenStore.Omnichannel.Shared.HttpClient.Storefront;

public interface IStorefrontApiClient
{
    CatalogClient Catalog { get; }
    CheckoutClient Checkout { get; }
    UserClient User { get; }
}

public class StorefrontApiClient : IStorefrontApiClient
{
    internal const string ApiClientKey = "OpenStoreApiClient";

    public StorefrontApiClient(IHttpClientFactory clientFactory)
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