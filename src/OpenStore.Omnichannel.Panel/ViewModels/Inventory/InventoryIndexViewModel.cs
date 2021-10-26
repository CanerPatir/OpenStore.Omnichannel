using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.Dto.Inventory;

namespace OpenStore.Omnichannel.Panel.ViewModels.Inventory;

public class InventoryIndexViewModel : BaseViewModel
{
    private readonly IApiClient _apiClient;

    public InventoryIndexViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<PagedListDto<InventoryListItemDto>> GetAll(PageRequest pageRequest) => await _apiClient.Inventory.GetAll(pageRequest);
}