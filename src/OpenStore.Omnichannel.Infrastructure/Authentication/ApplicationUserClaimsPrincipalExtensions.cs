using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using OpenIddict.Abstractions;

namespace OpenStore.Omnichannel.Infrastructure.Authentication;

public static class ApplicationUserClaimsPrincipalExtensions
{
    public static bool IsAdmin(this ClaimsPrincipal principal)
    {
        var roles = principal.Claims.Where(c => c.Type == OpenIddictConstants.Claims.Role);

        return roles.Any(r => r.Value.Equals(ApplicationRoles.Administrator, StringComparison.InvariantCultureIgnoreCase));
    }

    public static string GetFullName(this ClaimsPrincipal principal)
        => principal.Claims.FirstOrDefault(c => c.Type == OpenIddictConstants.Claims.Name)?.Value;

    public static string GetPhotoPath(this ClaimsPrincipal principal)
        => principal.Claims.FirstOrDefault(c => c.Type == OpenIddictConstants.Claims.Picture)?.Value;

    public static string GetEmail(this ClaimsPrincipal principal)
        => principal.Claims.FirstOrDefault(c => c.Type == OpenIddictConstants.Claims.Email)?.Value;

    public static IEnumerable<string> GetRoles(this ClaimsPrincipal principal)
        => principal.Claims.Where(c => c.Type == OpenIddictConstants.Claims.Role).Select(r => r.Value);

    public static Guid GetId(this ClaimsPrincipal principal)
    {
        var idClaim = principal.Claims.FirstOrDefault(c => c.Type == OpenIddictConstants.Claims.Subject)?.Value;

        return Guid.TryParse(idClaim, out var id) ? id : default;
    }
}