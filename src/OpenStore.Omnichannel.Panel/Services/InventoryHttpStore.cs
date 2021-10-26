using OpenStore.Omnichannel.Shared.Dto.Inventory;

namespace OpenStore.Omnichannel.Panel.Services;

public class InventoryHttpStore : HttpStore
{
    public InventoryHttpStore(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api/inventory";

    public async Task<PagedListDto<InventoryListItemDto>> GetAll(PageRequest pageRequest)
    {
        throw new NotImplementedException();
    }
}