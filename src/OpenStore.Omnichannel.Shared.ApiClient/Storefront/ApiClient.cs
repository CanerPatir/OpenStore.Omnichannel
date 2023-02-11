namespace OpenStore.Omnichannel.Shared.ApiClient.Storefront;

public interface IStorefrontApiClient
{
    CatalogClient Catalog { get; }
    CheckoutClient Checkout { get; }
    UserClient User { get; }
}

public class StorefrontApiClient : IStorefrontApiClient
{
    internal const string ClientName = "OpenStoreApiClient";

    public StorefrontApiClient(IHttpClientFactory clientFactory)
    {
        var httpClient = clientFactory.CreateClient(ClientName);
        Catalog = new CatalogClient(httpClient);
        Checkout = new CheckoutClient(httpClient);
        User = new UserClient(httpClient);
    }

    public CatalogClient Catalog { get; }
    public CheckoutClient Checkout { get; }
    public UserClient User { get; }
}