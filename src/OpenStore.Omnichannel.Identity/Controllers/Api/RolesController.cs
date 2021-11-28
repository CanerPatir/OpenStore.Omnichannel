using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Domain.IdentityContext;
using OpenStore.Omnichannel.Shared.Dto.Management;

namespace OpenStore.Omnichannel.Identity.Controllers.Api;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme, Roles = ApplicationRoles.Administrator)]
[Route("api/roles")]
public class RolesController : BaseCrudApiController<ApplicationRole, Guid, RoleDto>
{
    public RolesController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override Task Update(Guid id, RoleDto dto) => NotSupported();

    public override Task<object> Create(RoleDto dto)
    {
        NotSupported();
        return Task.FromResult(new object());
    }

    public override Task Delete(Guid id) => NotSupported();
}