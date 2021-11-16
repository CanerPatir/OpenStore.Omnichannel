using OpenStore.Omnichannel.Storefront.Models.Catalog;
using OpenStore.Omnichannel.Storefront.Services.Clients;

namespace OpenStore.Omnichannel.Storefront.Services;

public class ProductDetailViewModelFactory : IViewModelFactory
{
    private readonly IApiClient _apiClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductDetailViewModelFactory(IApiClient apiClient, IHttpContextAccessor httpContextAccessor)
    {
        _apiClient = apiClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ProductDetailViewModel> Produce(string handle, CancellationToken cancellationToken)
    {
        var productDetailResult = await _apiClient.Catalog.GetProductDetail(handle, cancellationToken);
        return new ProductDetailViewModel(productDetailResult);
    }
}