using System.Net.Http;

namespace OpenStore.Omnichannel.Storefront.Services.Clients;

public interface IApiClient
{
    CatalogStore Catalog { get; }
}

public class ApiClient : IApiClient
{
    internal const string apiClientKey = "OpenStoreApiClient";

    public ApiClient(IHttpClientFactory clientFactory)
    {
        var httpClient = clientFactory.CreateClient(apiClientKey);
        Catalog = new CatalogStore(httpClient);
    }

    public CatalogStore Catalog { get; }
}