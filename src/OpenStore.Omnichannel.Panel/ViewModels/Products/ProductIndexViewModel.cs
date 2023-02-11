using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.ApiClient.Management;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Panel.ViewModels.Products;

public class ProductIndexViewModel : BaseViewModel
{
    private readonly IApiClient _apiClient;

    public ProductIndexViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<PagedList<ProductListItemDto>> GetAll(PageRequest pageRequest) => await _apiClient.Product.GetAll(pageRequest);
    public async Task<PagedList<ProductListItemDto>> GetActive(PageRequest pageRequest) => await _apiClient.Product.GetActive(pageRequest);
    public async Task<PagedList<ProductListItemDto>> GetDraft(PageRequest pageRequest) => await _apiClient.Product.GetDraft(pageRequest);
    public async Task<PagedList<ProductListItemDto>> GetArchived(PageRequest pageRequest) => await _apiClient.Product.GetArchived(pageRequest);
}