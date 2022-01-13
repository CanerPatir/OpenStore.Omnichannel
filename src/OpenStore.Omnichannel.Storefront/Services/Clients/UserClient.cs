using System.Net.Http.Headers;
using OpenStore.Omnichannel.Shared.Dto.Identity;

namespace OpenStore.Omnichannel.Storefront.Services.Clients;

public class UserClient
{
    private readonly HttpClient _httpClient;

    public UserClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private string Path => "identity/api/users";

    public async Task<IEnumerable<ApplicationUserAddressDto>> GetMyAddresses(string token, CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        return await _httpClient.GetFromJsonAsync<List<ApplicationUserAddressDto>>($"{Path}/my-addresses", cancellationToken);
    }

    public Task AddAddressToMe(ApplicationUserAddressDto model, CancellationToken cancellationToken = default)
        => _httpClient.PostAsJsonAsync($"{Path}/my-addresses", model, cancellationToken);
}