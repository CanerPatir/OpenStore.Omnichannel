namespace OpenStore.Omnichannel.Shared.HttpClient.Management;

public abstract class HttpStore
{
    protected HttpStore(System.Net.Http.HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    protected System.Net.Http.HttpClient HttpClient { get; }

    protected abstract string Path { get; }

    protected virtual string GetPath(object route) => $"{Path}/{route}";

    // protected readonly JsonSerializerOptions DefaultSerializerOptions = new(JsonSerializerDefaults.Web)
    // {
    //     IgnoreNullValues = true
    // };
}