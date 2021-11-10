namespace OpenStore.Omnichannel.Infrastructure.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class RequiresStoreAuthorizeAttribute : RequiresAuthorizeAttribute
{
    public RequiresStoreAuthorizeAttribute()
    {
        Roles = string.Join(",", ApplicationRoles.Administrator, ApplicationRoles.StoreAdmin, ApplicationRoles.StoreOwner);
    }
}