using System.Net.Http.Json;
using OpenStore.Omnichannel.Shared.Dto.Store;

namespace OpenStore.Omnichannel.Panel.Services;

public class StoreHttpStore : HttpStore
{
    public StoreHttpStore(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api/store";

    public Task<StorePreferencesDto> GetStorePreferences() => HttpClient.GetFromJsonAsync<StorePreferencesDto>($"{Path}/preferences");

    public Task UpdateStorePreferences(StorePreferencesDto model) => HttpClient.PutAsJsonAsync($"{Path}/preferences", model);
}