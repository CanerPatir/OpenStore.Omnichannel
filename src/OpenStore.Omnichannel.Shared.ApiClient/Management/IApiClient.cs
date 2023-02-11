namespace OpenStore.Omnichannel.Shared.ApiClient.Management;

public interface IApiClient
{
    ProductClient Product { get; }
    MediaClient Media { get; }
    StoreClient Store { get; }
    InventoryHttpClient Inventory { get; }
    CollectionClient Collection { get; }
    OrderClient Order { get; }
    Task<bool> Ping();
}

public class ApiClient : IApiClient
{
    internal const string ClientName = "main";

    
    private readonly HttpClient _httpClient;

    public ProductClient Product { get; }
    public MediaClient Media { get; }
    public StoreClient Store { get; }
    public InventoryHttpClient Inventory { get; }
    public CollectionClient Collection { get; }
    public OrderClient Order { get; }

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        Product = new ProductClient(httpClient);
        Media = new MediaClient(httpClient);
        Store = new StoreClient(httpClient);
        Inventory = new InventoryHttpClient(httpClient);
        Collection = new CollectionClient(httpClient);
        Order = new OrderClient(httpClient);
    }

    public async Task<bool> Ping()
    {
        var response = await _httpClient.GetAsync("api/ping");
        return response.IsSuccessStatusCode;
    }
}