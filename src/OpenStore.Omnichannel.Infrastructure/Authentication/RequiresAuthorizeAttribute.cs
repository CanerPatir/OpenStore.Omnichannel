using Microsoft.AspNetCore.Authorization;
using OpenIddict.Validation.AspNetCore;

namespace OpenStore.Omnichannel.Infrastructure.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class RequiresAuthorizeAttribute : AuthorizeAttribute
{
    public RequiresAuthorizeAttribute()
    {
        AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
        Roles = null;
    }
}