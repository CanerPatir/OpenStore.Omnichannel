using System.Net.Http;

namespace OpenStore.Omnichannel.Storefront.Services.Clients;

public class CatalogStore : HttpStore
{
    public CatalogStore(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api-sf/catalog";
}