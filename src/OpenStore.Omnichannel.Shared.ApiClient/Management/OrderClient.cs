using System.Net.Http.Json;
using OpenStore.Omnichannel.Shared.Dto.Management.Order;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Shared.ApiClient.Management;

public class OrderClient : BaseClient
{
    protected override string Path => "api/orders";

    public OrderClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Guid> Create(OrderDto model)
    {
        var response = await HttpClient.PostAsJsonAsync($"{Path}", model);
        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    public Task Update(Guid orderId, OrderDto model) => HttpClient.PutAsJsonAsync($"{Path}/{orderId}", model);
    

    public Task<OrderDto> Get(Guid orderId) => HttpClient.GetFromJsonAsync<OrderDto>($"{Path}/{orderId}");

    public Task<PagedList<OrderListItemDto>> GetAll(PageRequest request) => HttpClient.GetPage<OrderListItemDto>($"{Path}/all", request);
}