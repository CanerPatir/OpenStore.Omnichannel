namespace OpenStore.Omnichannel.Shared.HttpClient.Management;

public interface IApiClient
{
    ProductHttpStore Product { get; }
    MediaHttpStore Media { get; }
    StoreHttpStore Store { get; }
    InventoryHttpStore Inventory { get; }
    CollectionHttpStore Collection { get; }
    OrderHttpStore Order { get; }
    Task<bool> Ping();
}

public class ApiClient : IApiClient
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public ProductHttpStore Product { get; }
    public MediaHttpStore Media { get; }
    public StoreHttpStore Store { get; }
    public InventoryHttpStore Inventory { get; }
    public CollectionHttpStore Collection { get; }
    public OrderHttpStore Order { get; }

    public ApiClient(System.Net.Http.HttpClient httpClient)
    {
        _httpClient = httpClient;
        Product = new ProductHttpStore(httpClient);
        Media = new MediaHttpStore(httpClient);
        Store = new StoreHttpStore(httpClient);
        Inventory = new InventoryHttpStore(httpClient);
        Collection = new CollectionHttpStore(httpClient);
        Order = new OrderHttpStore(httpClient);
    }

    public async Task<bool> Ping()
    {
        var response = await _httpClient.GetAsync("api/ping");
        return response.IsSuccessStatusCode;
    }
}