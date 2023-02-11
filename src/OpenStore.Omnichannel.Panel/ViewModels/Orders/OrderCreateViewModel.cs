using Microsoft.AspNetCore.Components;
using OpenStore.Omnichannel.Shared.Dto.Management.Order;
using OpenStore.Omnichannel.Shared.HttpClient.Management;

namespace OpenStore.Omnichannel.Panel.ViewModels.Orders;

public class OrderCreateViewModel : OrderViewModelBase
{
    private readonly NavigationManager _navigationManager;
    private bool _saving;

    public OrderCreateViewModel(IApiClient apiClient, NavigationManager navigationManager) : base(apiClient)
    {
        _navigationManager = navigationManager;
    }

    public bool Saving
    {
        get => _saving;
        private set => SetValue(ref _saving, value);
    }

    public async Task Create()
    {
        Saving = true;
        try
        {
            var orderId = await ApiClient.Order.Create(Order);
            _navigationManager.NavigateTo($"orders/{orderId}");
        }
        finally
        {
            Saving = false;
        }
    }

    public void Init()
    {
        Order = new OrderDto();
    }
}