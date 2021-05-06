using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OpenStore.Omnichannel.Panel.Services;

// ReSharper disable CheckNamespace

namespace OpenStore.Omnichannel.Panel
{
    public static class HttpClientExtensions
    {
        public static async Task<PagedListDto<T>> GetPage<T>(this HttpClient httpClient, string path, int pageNumber, int pageSize)
        {
            var qs = $"?pageNumber={pageNumber}&pageSize={pageSize}";
            var response = await httpClient.GetAsync(path + qs);
            return await response.Content.ReadFromJsonAsync<PagedListDto<T>>();
        }
    }
}