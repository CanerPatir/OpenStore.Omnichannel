using OpenStore.Omnichannel.Shared.Dto.Management.Inventory;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Panel.Services;

public class InventoryHttpStore : HttpStore
{
    public InventoryHttpStore(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api/inventory";

    public Task<PagedList<InventoryListItemDto>> GetAll(PageRequest request) => HttpClient.GetPage<InventoryListItemDto>($"{Path}/all", request);

    public Task SetStock(Guid inventoryId, int quantity) => HttpClient.PostAsync($"api/Inventory/{inventoryId}/set-stock/{quantity}", null);

    public Task AddStock(Guid inventoryId, int quantity) => HttpClient.PostAsync($"api/Inventory/{inventoryId}/add-stock/{quantity}", null);
}