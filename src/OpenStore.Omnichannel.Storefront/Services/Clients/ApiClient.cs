using System.Net.Http;

namespace OpenStore.Omnichannel.Storefront.Services.Clients
{
    public interface IApiClient
    {
        CatalogStore Catalog { get; }
    }

    public class ApiClient : IApiClient
    {
        public ApiClient(IHttpClientFactory clientFactory)
        {
            var httpClient = clientFactory.CreateClient(Startup.ApiClientKey);
            Catalog = new CatalogStore(httpClient);
        }

        public CatalogStore Catalog { get; }
    }
}