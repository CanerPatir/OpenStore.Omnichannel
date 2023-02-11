namespace OpenStore.Omnichannel.Shared.ApiClient;

public abstract class BaseClient
{
    protected BaseClient(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    protected HttpClient HttpClient { get; }

    protected abstract string Path { get; }

    protected static string Combine(string path, object route) => $"{path.Trim('/')}/{(route is string strRoute ? strRoute.Trim('/') : route)}";
    
    protected string GetPath(object route) => Combine(Path, route);

    // protected readonly JsonSerializerOptions DefaultSerializerOptions = new(JsonSerializerDefaults.Web)
    // {
    //     IgnoreNullValues = true
    // };
}