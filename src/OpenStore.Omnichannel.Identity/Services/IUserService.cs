using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.IdentityContext;
using OpenStore.Omnichannel.Shared.Dto.Identity;
using OpenStore.Omnichannel.Shared.Dto.Management;
using OpenStore.Omnichannel.Shared.Request;

namespace OpenStore.Omnichannel.Identity.Services;

public interface IUserService : ICrudService<ApplicationUser, ApplicationUserDto>
{
    Task AddToRole(Guid userId, string role, CancellationToken cancellationToken = default);
    Task RemoveFromRole(Guid userId, string role, CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> GetUserRoles(Guid id, CancellationToken cancellationToken = default);
    Task RevokeUserToken(Guid userId, CancellationToken cancellationToken = default);
    Task ChangePassword(Guid userId, ChangePasswordRequest model, CancellationToken cancellationToken);
    Task<IEnumerable<ApplicationUserAddressDto>> GetAddresses(Guid userId, CancellationToken cancellationToken);
    Task AddAddress(Guid userId, ApplicationUserAddressDto model, CancellationToken cancellationToken);
    Task UpdateAddress(Guid userId, ApplicationUserAddressDto model, CancellationToken cancellationToken);
}