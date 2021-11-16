using OpenStore.Omnichannel.Shared.Query.Result;
using OpenStore.Omnichannel.Storefront.Services.Clients;

namespace OpenStore.Omnichannel.Storefront.Services;

public class ProductBffService : IBffService
{
    private const int BatchSize = 50;

    private readonly IApiClient _apiClient;

    public ProductBffService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<IEnumerable<ProductItemDto>> GetSearchProductsPage(string term, int page, CancellationToken cancellationToken = default)
    {
        var getAllProductsResult = await _apiClient.Catalog.GetSearchProducts(term, BatchSize, page == 1 ? 0 : BatchSize * page, cancellationToken);
        return getAllProductsResult.Items;
    }

    public async Task<IEnumerable<ProductItemDto>> GetCollectionProductsPage(Guid collectionId, int page, CancellationToken cancellationToken = default)
    {
        var getAllProductsResult = await _apiClient.Catalog.GetCollectionProducts(collectionId, BatchSize, page == 1 ? 0 : BatchSize * page, cancellationToken);
        return getAllProductsResult.Items;
    }

    public async Task<IEnumerable<ProductItemDto>> GetAllProductsPage(int page, CancellationToken cancellationToken = default)
    {
        var getAllProductsResult = await _apiClient.Catalog.GetAllProducts(BatchSize, page == 1 ? 0 : BatchSize * page, cancellationToken);
        return getAllProductsResult.Items;
    }
}