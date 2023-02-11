using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using OpenStore.Omnichannel;
using OpenStore.Shared;

// ReSharper disable CheckNamespace

namespace System.Net.Http;

public static class HttpClientExtensions
{
    public static async Task<PagedList<T>> GetPage<T>(this HttpClient httpClient, string path, PageRequest request)
    {
        var query = new Dictionary<string, string>
        {
            { "PageNumber", request.PageNumber.ToString() },
            { "PageSize", request.PageSize.ToString() },
        };
        if (request.SortColumn != null)
        {
            query.Add("SortColumn", request.SortColumn);
            query.Add("SortDirection", request.SortDirection.ToString());
        }

        if (request.FilterTerm != null)
        {
            query.Add("FilterTerm", request.FilterTerm);
        }

        return await httpClient.GetFromJsonAsync<PagedList<T>>(QueryHelpers.AddQueryString(path, query));
    }
}