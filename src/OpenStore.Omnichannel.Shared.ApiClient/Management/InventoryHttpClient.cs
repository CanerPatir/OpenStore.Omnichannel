using OpenStore.Omnichannel.Shared.Query.Management.InventoryContext.Result;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Shared.ApiClient.Management;

public class InventoryHttpClient : BaseClient
{
    public InventoryHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api/inventory";

    public Task<PagedList<InventoryListItemDto>> GetAll(PageRequest request) => HttpClient.GetPage<InventoryListItemDto>($"{Path}/all", request);

    public Task SetStock(Guid inventoryId, int quantity) => HttpClient.PostAsync($"api/Inventory/{inventoryId}/set-stock/{quantity}", null);

    public Task AddStock(Guid inventoryId, int quantity) => HttpClient.PostAsync($"api/Inventory/{inventoryId}/add-stock/{quantity}", null);
}