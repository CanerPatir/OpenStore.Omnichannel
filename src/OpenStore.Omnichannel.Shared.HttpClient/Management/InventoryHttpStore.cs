using OpenStore.Omnichannel.Shared.Query.Management.InventoryContext.Result;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Shared.HttpClient.Management;

public class InventoryHttpStore : HttpStore
{
    public InventoryHttpStore(System.Net.Http.HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api/inventory";

    public Task<PagedList<InventoryListItemDto>> GetAll(PageRequest request) => HttpClient.GetPage<InventoryListItemDto>($"{Path}/all", request);

    public Task SetStock(Guid inventoryId, int quantity) => HttpClient.PostAsync($"api/Inventory/{inventoryId}/set-stock/{quantity}", null);

    public Task AddStock(Guid inventoryId, int quantity) => HttpClient.PostAsync($"api/Inventory/{inventoryId}/add-stock/{quantity}", null);
}