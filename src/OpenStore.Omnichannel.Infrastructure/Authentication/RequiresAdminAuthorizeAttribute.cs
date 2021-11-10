namespace OpenStore.Omnichannel.Infrastructure.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class RequiresAdminAuthorizeAttribute : RequiresAuthorizeAttribute
{
    public RequiresAdminAuthorizeAttribute()
    {
        Roles = ApplicationRoles.Administrator;
    }
}