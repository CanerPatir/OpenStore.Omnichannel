using System.Net.Http;

namespace OpenStore.Omnichannel.Panel.Services
{
    public abstract class HttpStore
    {
        protected HttpStore(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        protected HttpClient HttpClient { get; }

        protected abstract string Path { get; }

        protected virtual string GetPath(object route) => $"{Path}/{route}";

    }
}