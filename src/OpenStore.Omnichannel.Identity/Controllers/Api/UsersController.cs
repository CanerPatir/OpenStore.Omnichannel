using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Domain.IdentityContext;
using OpenStore.Omnichannel.Identity.Services;
using OpenStore.Omnichannel.Infrastructure.Authentication;
using OpenStore.Omnichannel.Shared.Dto.Identity;
using OpenStore.Omnichannel.Shared.Request.IdentityContext;

namespace OpenStore.Omnichannel.Identity.Controllers.Api;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
[Route("api/users")]
public class UsersController : BaseCrudApiController<ApplicationUser, Guid, ApplicationUserDto>
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _userService = userService;
    }

    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Roles = ApplicationRoles.Administrator)]
    [HttpPost("{id:guid}/revoke")]
    public async Task RevokeUserToken(Guid id) => await _userService.RevokeUserToken(id, CancellationToken);

    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Roles = ApplicationRoles.Administrator)]
    [HttpGet("{id:guid}/roles")]
    public Task<IEnumerable<string>> GetUserRoles(Guid id) => _userService.GetUserRoles(id, CancellationToken);

    [HttpPost("change-password")]
    public Task ChangePassword(ChangePasswordRequest model)
    {
        ThrowIfModelInvalid();
        return _userService.ChangePassword(User.GetId(), model, CancellationToken);
    }

    [HttpGet("my-addresses")]
    public Task<IEnumerable<ApplicationUserAddressDto>> Addresses() => _userService.GetAddresses(User.GetId(), CancellationToken);

    [HttpPost("my-addresses")]
    public Task AddAddress(ApplicationUserAddressDto model) => _userService.AddAddress(User.GetId(), model, CancellationToken);

    [HttpPut("my-addresses")]
    public Task UpdateAddresses(ApplicationUserAddressDto model) => _userService.UpdateAddress(User.GetId(), model, CancellationToken);
}