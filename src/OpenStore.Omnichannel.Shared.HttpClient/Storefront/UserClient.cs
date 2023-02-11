using System.Net.Http.Json;
using OpenStore.Omnichannel.Shared.Dto.Identity;

namespace OpenStore.Omnichannel.Shared.HttpClient.Storefront;

public class UserClient
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public UserClient(System.Net.Http.HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private string Path => "identity/api/users";

    public async Task<IEnumerable<ApplicationUserAddressDto>> GetMyAddresses(CancellationToken cancellationToken = default) 
        => await _httpClient.GetFromJsonAsync<List<ApplicationUserAddressDto>>($"{Path}/my-addresses", cancellationToken);

    public Task AddAddressToMe(ApplicationUserAddressDto model, CancellationToken cancellationToken = default)
        => _httpClient.PostAsJsonAsync($"{Path}/my-addresses", model, cancellationToken);

    public Task UpdateAddress(ApplicationUserAddressDto model, CancellationToken cancellationToken)
        => _httpClient.PutAsJsonAsync($"{Path}/my-addresses", model, cancellationToken);
}