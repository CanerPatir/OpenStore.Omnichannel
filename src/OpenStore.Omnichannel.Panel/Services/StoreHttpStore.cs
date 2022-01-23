using System.Net.Http.Json;
using OpenStore.Omnichannel.Shared.Query.Management.StoreContext.Result;

namespace OpenStore.Omnichannel.Panel.Services;

public class StoreHttpStore : HttpStore
{
    public StoreHttpStore(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api/store";

    public Task<StorePreferencesQueryResult> GetStorePreferences() => HttpClient.GetFromJsonAsync<StorePreferencesQueryResult>($"{Path}/preferences");

    public Task UpdateStorePreferences(StorePreferencesQueryResult model) => HttpClient.PutAsJsonAsync($"{Path}/preferences", model);
}