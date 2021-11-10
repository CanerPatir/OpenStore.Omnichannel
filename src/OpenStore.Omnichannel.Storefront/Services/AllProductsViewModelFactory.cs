using OpenStore.Omnichannel.Storefront.Models.Catalog;
using OpenStore.Omnichannel.Storefront.Services.Clients;

namespace OpenStore.Omnichannel.Storefront.Services;

public class AllProductsViewModelFactory : IViewModelFactory<AllProductsViewModel>
{
    private const int BatchSize = 50;

    private readonly IApiClient _apiClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AllProductsViewModelFactory(IApiClient apiClient, IHttpContextAccessor httpContextAccessor)
    {
        _apiClient = apiClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AllProductsViewModel> Produce(CancellationToken cancellationToken = default)
    {
        var firstIndex = 0;
        var getAllProductsResult = await _apiClient.Catalog.GetAllProducts(BatchSize, firstIndex);

        return new AllProductsViewModel(getAllProductsResult.Items.Select(x => new  ProductItemViewModel()).ToList());
    }
}