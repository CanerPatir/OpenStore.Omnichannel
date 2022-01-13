using Microsoft.AspNetCore.Authentication;
using OpenIddict.Abstractions;
using OpenStore.Omnichannel.Shared.Dto.Identity;
using OpenStore.Omnichannel.Storefront.Services.Clients;

namespace OpenStore.Omnichannel.Storefront.Services;

public class UserBffService : BffService
{
    private readonly IApiClient _apiClient;

    public UserBffService(IApiClient apiClient, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _apiClient = apiClient;
    }

    public async Task<IEnumerable<ApplicationUserAddressDto>> GetMyAddresses(CancellationToken cancellationToken = default)
    {
        string accessToken = await HttpContext.GetTokenAsync("access_token");
        string idToken = await HttpContext.GetTokenAsync("id_token");
        return await _apiClient.User.GetMyAddresses(accessToken, cancellationToken);
    }

    public Task AddAddressToMe(ApplicationUserAddressDto model, CancellationToken cancellationToken = default)
        => _apiClient.User.AddAddressToMe(model, cancellationToken);
}