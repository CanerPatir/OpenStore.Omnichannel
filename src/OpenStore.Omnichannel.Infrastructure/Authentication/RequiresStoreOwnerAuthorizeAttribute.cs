namespace OpenStore.Omnichannel.Infrastructure.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class RequiresStoreOwnerAuthorizeAttribute : RequiresAuthorizeAttribute
{
    public RequiresStoreOwnerAuthorizeAttribute()
    {
        Roles = string.Join(",", ApplicationRoles.Administrator, ApplicationRoles.StoreOwner);
    }
}