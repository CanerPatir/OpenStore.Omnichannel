using OpenStore.Omnichannel.Shared.Dto.Identity;
using OpenStore.Omnichannel.Shared.HttpClient.Storefront;

namespace OpenStore.Omnichannel.Storefront.Services;

public class UserBffService : BffService
{
    private readonly IStorefrontApiClient _apiClient;

    public UserBffService(IStorefrontApiClient apiClient, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _apiClient = apiClient;
    }

    public async Task<IEnumerable<ApplicationUserAddressDto>> GetMyAddresses(CancellationToken cancellationToken = default)
    {
        return await _apiClient.User.GetMyAddresses(cancellationToken);
    }

    public Task AddAddressToMe(ApplicationUserAddressDto model, CancellationToken cancellationToken = default)
        => _apiClient.User.AddAddressToMe(model, cancellationToken);

    public  Task UpdateAddress(ApplicationUserAddressDto model, CancellationToken cancellationToken)
        => _apiClient.User.UpdateAddress(model, cancellationToken);

}