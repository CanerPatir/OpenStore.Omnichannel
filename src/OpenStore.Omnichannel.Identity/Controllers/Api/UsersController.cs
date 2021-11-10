using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Domain.IdentityContext;
using OpenStore.Omnichannel.Identity.Services;
using OpenStore.Omnichannel.Infrastructure.Authentication;
using OpenStore.Omnichannel.Shared.Dto;
using OpenStore.Omnichannel.Shared.Dto.Management;
using OpenStore.Omnichannel.Shared.Request;

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
    [HttpPost("{id}/revoke")]
    public async Task RevokeUserToken(Guid id) => await _userService.RevokeUserToken(id, CancellationToken);

    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Roles = ApplicationRoles.Administrator)]
    [HttpGet("{id}/roles")]
    public Task<IEnumerable<string>> GetUserRoles(Guid id) => _userService.GetUserRoles(id, CancellationToken);

    [HttpPost("change-password")]
    public Task ChangePassword(ChangePasswordRequest model)
    {
        ThrowIfModelInvalid();
        return _userService.ChangePassword(User.GetId(), model, CancellationToken);
    }
}