using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.Dto.Management.Inventory;

namespace OpenStore.Omnichannel.Panel.ViewModels.Inventory;

public class InventoryIndexViewModel : BaseViewModel
{
    private bool _saving;
    private readonly IApiClient _apiClient;

    public InventoryIndexViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    
    public bool Saving
    {
        get => _saving;
        private set => SetValue(ref _saving, value);
    }

    public async Task<PagedListDto<InventoryListItemDto>> GetAll(PageRequest pageRequest) => await _apiClient.Inventory.GetAll(pageRequest);

    public async Task SetStock(InventoryListItemDto item, int quantity)
    {
        Saving = true;
        try
        {
            await _apiClient.Inventory.SetStock(item.InventoryId, quantity);
        }
        finally
        {
            Saving = false;
        }
    } 
    
    public async Task AddStock(InventoryListItemDto item, int quantity)
    {
        Saving = true;
        try
        {
            await _apiClient.Inventory.AddStock(item.InventoryId, quantity);
        }
        finally
        {
            Saving = false;
        }
    } 
}