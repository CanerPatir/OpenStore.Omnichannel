using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.Dto.Management.Order;

namespace OpenStore.Omnichannel.Panel.ViewModels.Orders;

public abstract class OrderViewModelBase : BaseViewModel
{
    private OrderDto _order;

    public virtual OrderDto Order
    {
        get => _order;
        protected set => SetValue(ref _order, value);
    }

    public bool IsNull => Order is null;

    protected IApiClient ApiClient { get; }

    protected OrderViewModelBase(IApiClient apiClient)
    {
        ApiClient = apiClient;
    }
}