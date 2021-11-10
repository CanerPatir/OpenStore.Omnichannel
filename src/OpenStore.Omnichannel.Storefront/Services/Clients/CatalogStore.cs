using OpenStore.Omnichannel.Shared.Query.Result;

namespace OpenStore.Omnichannel.Storefront.Services.Clients;

public class CatalogStore : HttpStore
{
    public CatalogStore(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api-sf/catalog";

    public Task<AllProductsResult> GetAllProducts(int batchSize, int firstIndex) 
        => HttpClient.GetFromJsonAsync<AllProductsResult>($"{Path}/all/{batchSize}/{firstIndex}");
    
    public Task<CollectionProductsResult> GetCollectionProducts(Guid id, int batchSize, int firstIndex) 
        => HttpClient.GetFromJsonAsync<CollectionProductsResult>($"{Path}/collection/{id}/?batchSize={batchSize}&firstIndex={firstIndex}");
}