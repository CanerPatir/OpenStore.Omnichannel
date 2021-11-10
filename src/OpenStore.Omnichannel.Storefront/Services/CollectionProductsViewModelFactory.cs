using OpenStore.Omnichannel.Storefront.Models.Catalog;
using OpenStore.Omnichannel.Storefront.Services.Clients;

namespace OpenStore.Omnichannel.Storefront.Services;

public class CollectionProductsViewModelFactory : IViewModelFactory<CollectionProductsViewModel>
{
    private const int BatchSize = 50;

    private readonly IApiClient _apiClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CollectionProductsViewModelFactory(IApiClient apiClient, IHttpContextAccessor httpContextAccessor)
    {
        _apiClient = apiClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CollectionProductsViewModel> Produce(CancellationToken cancellationToken = default)
    {
        var firstIndex = 0;
        var collectionId = Guid.Empty;
        var getCollectionProductsResult = await _apiClient.Catalog.GetCollectionProducts(collectionId, BatchSize, firstIndex);

        return new CollectionProductsViewModel(getCollectionProductsResult.Items.Select(x => new  ProductItemViewModel()).ToList());
    }
}