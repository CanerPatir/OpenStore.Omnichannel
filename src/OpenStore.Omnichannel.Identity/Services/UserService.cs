using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Core;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Data.EntityFramework.Crud;
using OpenStore.Omnichannel.Domain.IdentityContext;
using OpenStore.Omnichannel.Shared.Dto.Management;
using OpenStore.Omnichannel.Shared.Request;

namespace OpenStore.Omnichannel.Identity.Services;

public class UserService : EntityFrameworkCrudService<ApplicationUser, ApplicationUserDto>, IUserService
{
    private readonly OpenIddictTokenManager<ApplicationToken> _tokenManager;
    private readonly OpenIddictAuthorizationManager<ApplicationAuthorization> _authorizationManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(
        OpenIddictTokenManager<ApplicationToken> tokenManager,
        OpenIddictAuthorizationManager<ApplicationAuthorization> authorizationManager,
        UserManager<ApplicationUser> userManager,
        ICrudRepository<ApplicationUser> repository,
        IOpenStoreObjectMapper mapper) : base(repository, mapper)
    {
        _tokenManager = tokenManager;
        _authorizationManager = authorizationManager;
        _userManager = userManager;
    }

    public async Task AddToRole(Guid userId, string role, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken);
        var result = await _userManager.AddToRoleAsync(user, role);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(",", result.Errors.Select(x => x.Description)));
        }
    }

    public async Task RemoveFromRole(Guid userId, string role, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken);
        var result = await _userManager.RemoveFromRoleAsync(user, role);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(",", result.Errors.Select(x => x.Description)));
        }
    }

    public async Task<IEnumerable<string>> GetUserRoles(Guid id, CancellationToken cancellationToken = default)
    {
        return await _userManager.GetRolesAsync(new ApplicationUser
        {
            Id = id
        });
    }

    /// <summary>
    /// Invalidates tokens and security stamp
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task RevokeUserToken(Guid userId, CancellationToken cancellationToken = default)
    {
        await foreach (var auth in _authorizationManager.FindBySubjectAsync(userId.ToString(), cancellationToken))
        {
            await _authorizationManager.DeleteAsync(auth, cancellationToken);
        }

        await foreach (var token in _tokenManager.FindBySubjectAsync(userId.ToString(), cancellationToken))
        {
            await _tokenManager.TryRevokeAsync(token, cancellationToken);
        }

        // await _userManager.UpdateSecurityStampAsync(await _userManager.FindByIdAsync(userId.ToString())); 
    }

    public async Task ChangePassword(Guid userId, ChangePasswordRequest model, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (!result.Succeeded)
        {
            throw new ApplicationException(string.Join(",", Msg.Application.PasswordChangeError, result.Errors.Select(x => x.Description)));
        }
    }
}