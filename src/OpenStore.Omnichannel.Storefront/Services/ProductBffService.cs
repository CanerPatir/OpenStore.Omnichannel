using OpenStore.Omnichannel.Shared.HttpClient.Storefront;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;
using OpenStore.Omnichannel.Storefront.Models.Catalog;

namespace OpenStore.Omnichannel.Storefront.Services;

public class ProductBffService : BffService
{
    private const int BatchSize = 50;

    private readonly IStorefrontApiClient _apiClient;

    public ProductBffService(IStorefrontApiClient apiClient, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
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

    public async Task<ProductDetailViewModel> GetProductDetailViewModel(string handle, CancellationToken cancellationToken)
    {
        var productDetailResult = await _apiClient.Catalog.GetProductDetail(handle, cancellationToken);
        return new ProductDetailViewModel(productDetailResult);
    }
}