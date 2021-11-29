namespace OpenStore.Omnichannel.Storefront.Services.Clients;

public abstract class BaseClient
{
    protected BaseClient(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    protected HttpClient HttpClient { get; }

    protected abstract string Path { get; }

    protected virtual string GetPath(object route) => $"{Path}/{route}";

    // protected readonly JsonSerializerOptions DefaultSerializerOptions = new(JsonSerializerDefaults.Web)
    // {
    //     IgnoreNullValues = true
    // };
}