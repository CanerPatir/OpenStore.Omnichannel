using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Panel.ViewModels.Products;

public class ProductIndexViewModel : BaseViewModel
{
    private readonly IApiClient _apiClient;

    public ProductIndexViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<PagedListDto<ProductListItemDto>> GetAll(PageRequest pageRequest) => await _apiClient.Product.GetAll(pageRequest);
    public async Task<PagedListDto<ProductListItemDto>> GetActive(PageRequest pageRequest) => await _apiClient.Product.GetActive(pageRequest);
    public async Task<PagedListDto<ProductListItemDto>> GetDraft(PageRequest pageRequest) => await _apiClient.Product.GetDraft(pageRequest);
    public async Task<PagedListDto<ProductListItemDto>> GetArchived(PageRequest pageRequest) => await _apiClient.Product.GetArchived(pageRequest);
}