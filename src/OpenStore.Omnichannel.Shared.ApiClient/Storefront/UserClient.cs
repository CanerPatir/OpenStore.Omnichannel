using System.Net.Http.Json;
using OpenStore.Omnichannel.Shared.Dto.Identity;

namespace OpenStore.Omnichannel.Shared.ApiClient.Storefront;

public class UserClient : BaseClient
{
    public UserClient(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "identity/api/users";

    public async Task<IEnumerable<ApplicationUserAddressDto>> GetMyAddresses(CancellationToken cancellationToken = default)
        => await HttpClient.GetFromJsonAsync<List<ApplicationUserAddressDto>>(GetPath("my-addresses"), cancellationToken);

    public Task AddAddressToMe(ApplicationUserAddressDto model, CancellationToken cancellationToken = default)
        => HttpClient.PostAsJsonAsync(GetPath("my-addresses"), model, cancellationToken);

    public Task UpdateAddress(ApplicationUserAddressDto model, CancellationToken cancellationToken)
        => HttpClient.PutAsJsonAsync(GetPath("my-addresses"), model, cancellationToken);
}