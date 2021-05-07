using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using OpenStore.Omnichannel;

// ReSharper disable CheckNamespace

namespace System.Net.Http
{
    public static class HttpClientExtensions
    {
        public static async Task<PagedListDto<T>> GetPage<T>(this HttpClient httpClient, string path, PageRequest request)
        {
            var response = await httpClient.PostAsJsonAsync(path, request);
            return await response.Content.ReadFromJsonAsync<PagedListDto<T>>();
        }
    }
}