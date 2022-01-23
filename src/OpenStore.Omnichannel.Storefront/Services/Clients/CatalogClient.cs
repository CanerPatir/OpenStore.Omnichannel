using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.Storefront.Services.Clients;

public class CatalogClient
{
    public CatalogClient(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    private HttpClient HttpClient { get; }

    private string Path => "api-sf/catalog";

    public Task<CollectionProductsQueryResult> GetSearchProducts(string term, int batchSize, int firstIndex, CancellationToken cancellationToken = default)
        => HttpClient.GetFromJsonAsync<CollectionProductsQueryResult>($"{Path}/search/{term}/?batchSize={batchSize}&firstIndex={firstIndex}", cancellationToken);

    public Task<CollectionProductsQueryResult> GetCollectionProducts(Guid id, int batchSize, int firstIndex, CancellationToken cancellationToken = default)
        => HttpClient.GetFromJsonAsync<CollectionProductsQueryResult>($"{Path}/collection/{id}/?batchSize={batchSize}&firstIndex={firstIndex}", cancellationToken);

    public Task<AllProductsQueryResult> GetAllProducts(int batchSize, int firstIndex, CancellationToken cancellationToken = default)
        => HttpClient.GetFromJsonAsync<AllProductsQueryResult>($"{Path}/all/{batchSize}/{firstIndex}", cancellationToken);

    public Task<ProductDetailQueryResult> GetProductDetail(string handle, CancellationToken cancellationToken)
        => HttpClient.GetFromJsonAsync<ProductDetailQueryResult>($"{Path}/product/{handle}", cancellationToken);
}