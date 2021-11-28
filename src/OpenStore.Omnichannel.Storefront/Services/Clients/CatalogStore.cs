using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.Storefront.Services.Clients;

public class CatalogStore : HttpStore
{
    public CatalogStore(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api-sf/catalog";
    
    public Task<CollectionProductsResult> GetSearchProducts(string term, int batchSize, int firstIndex, CancellationToken cancellationToken = default) 
        => HttpClient.GetFromJsonAsync<CollectionProductsResult>($"{Path}/search/{term}/?batchSize={batchSize}&firstIndex={firstIndex}", cancellationToken);
    
    public Task<CollectionProductsResult> GetCollectionProducts(Guid id, int batchSize, int firstIndex, CancellationToken cancellationToken = default) 
        => HttpClient.GetFromJsonAsync<CollectionProductsResult>($"{Path}/collection/{id}/?batchSize={batchSize}&firstIndex={firstIndex}", cancellationToken); 
    
    public Task<AllProductsResult> GetAllProducts(int batchSize, int firstIndex, CancellationToken cancellationToken = default) 
        => HttpClient.GetFromJsonAsync<AllProductsResult>($"{Path}/all/{batchSize}/{firstIndex}", cancellationToken);

    public Task<ProductDetailResult> GetProductDetail(string handle, CancellationToken cancellationToken)
        => HttpClient.GetFromJsonAsync<ProductDetailResult>($"{Path}/product/{handle}", cancellationToken);
}