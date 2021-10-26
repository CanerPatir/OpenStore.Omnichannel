using System.Security.Claims;
using OpenStore.Omnichannel.Panel.Extensions.Authorization;

// ReSharper disable CheckNamespace

namespace OpenStore.Omnichannel.Panel;

public static class ClaimsPrincipleExtensions
{
    public static Guid GetId(this ClaimsPrincipal claimsPrincipal)
    {
        var id = claimsPrincipal.Claims.Single(x => x.Type == "sub");
        return Guid.Parse(id.Value);
    }

    public static IEnumerable<string> GetRoles(this ClaimsPrincipal claimsPrincipal)
    {
        var userRoleClaim = claimsPrincipal.Claims.SingleOrDefault(x => x.Type == "role");

        if (userRoleClaim == null)
        {
            throw new UserRoleClaimNotFoundException();
        }

        return GetRolesFromClaim(userRoleClaim.Value);
    }

    public static bool IsRolesEmpty(this ClaimsPrincipal claimsPrincipal)
    {
        try
        {
            claimsPrincipal.GetRoles();
            return false;
        }
        catch (UserRoleClaimNotFoundException)
        {
            return true;
        }
    }

    public static bool InRole(this ClaimsPrincipal claimsPrincipal, string role)
    {
        try
        {
            var userRoles = claimsPrincipal.GetRoles();
            return userRoles.Any(r => r.Equals(role, StringComparison.InvariantCultureIgnoreCase));
        }
        catch (UserRoleClaimNotFoundException)
        {
            return false;
        }
    }

    private static IEnumerable<string> GetRolesFromClaim(string userRoleValue)
    {
        return userRoleValue.Trim('[').Trim(']').Trim().Split(',').Select(x => x.Trim('"').Trim());
    }
}