using OpenStore.Omnichannel.Panel.Services;

namespace OpenStore.Omnichannel.Panel.ViewModels.Orders;

public class OrderUpdateViewModel : OrderViewModelBase
{
    private readonly DialogService _dialogService;
    private bool _saving;

    public OrderUpdateViewModel(IApiClient apiClient, DialogService dialogService) : base(apiClient)
    {
        _dialogService = dialogService;
    }

    public bool Saving
    {
        get => _saving;
        private set => SetValue(ref _saving, value);
    }
    
    // ReSharper disable once PossibleInvalidOperationException
    public Guid OrderId => Order.Id.Value;

    public async Task Retrieve(Guid id)
    {
        Order = await ApiClient.Order.Get(id);
    }

    public async Task Update()
    {
        Saving = true;
        try
        {
            await ApiClient.Order.Update(OrderId, Order);
        }
        finally
        {
            Saving = false;
        }
    }
}