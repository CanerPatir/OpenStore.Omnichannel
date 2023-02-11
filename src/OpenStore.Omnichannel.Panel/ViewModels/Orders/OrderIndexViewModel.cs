using OpenStore.Omnichannel.Shared.Dto.Management.Order;
using OpenStore.Omnichannel.Shared.ApiClient.Management;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Panel.ViewModels.Orders;

public class OrderIndexViewModel : BaseViewModel
{
    private readonly IApiClient _apiClient;

    public OrderIndexViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    
    public async Task<PagedList<OrderListItemDto>> GetAll(PageRequest pageRequest) => await _apiClient.Order.GetAll(pageRequest);

}