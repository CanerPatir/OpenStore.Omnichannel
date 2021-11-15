using OpenStore.Omnichannel.Storefront.Models.Catalog;
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

    public async Task<AllProductsPageDto> GetProductsPage(int page, CancellationToken cancellationToken = default)
    {
        var firstIndex = 0; 
        var getAllProductsResult = await _apiClient.Catalog.GetAllProducts(BatchSize, firstIndex);
        return new AllProductsPageDto(getAllProductsResult.Items.Select(x => new  ProductItemDto(x.Id, x.Title, x.PhotoUrl)).ToList());
    }
}