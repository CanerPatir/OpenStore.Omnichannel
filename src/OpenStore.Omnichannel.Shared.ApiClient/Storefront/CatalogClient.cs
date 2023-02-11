using System.Net.Http.Json;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.Shared.ApiClient.Storefront;

public class CatalogClient : BaseClient
{
    public CatalogClient(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api-sf/catalog";

    public Task<CollectionProductsQueryResult> GetSearchProducts(string term, int batchSize, int firstIndex, CancellationToken cancellationToken = default)
        => HttpClient.GetFromJsonAsync<CollectionProductsQueryResult>(GetPath($"search/{term}/?batchSize={batchSize}&firstIndex={firstIndex}"), cancellationToken);

    public Task<CollectionProductsQueryResult> GetCollectionProducts(Guid id, int batchSize, int firstIndex, CancellationToken cancellationToken = default)
        => HttpClient.GetFromJsonAsync<CollectionProductsQueryResult>(GetPath($"collection/{id}/?batchSize={batchSize}&firstIndex={firstIndex}"), cancellationToken);

    public Task<AllProductsQueryResult> GetAllProducts(int batchSize, int firstIndex, CancellationToken cancellationToken = default)
        => HttpClient.GetFromJsonAsync<AllProductsQueryResult>(GetPath($"all/{batchSize}/{firstIndex}"), cancellationToken);

    public Task<ProductDetailQueryResult> GetProductDetail(string handle, CancellationToken cancellationToken)
        => HttpClient.GetFromJsonAsync<ProductDetailQueryResult>(GetPath($"product/{handle}"), cancellationToken);
}