using System.Security.Claims;

namespace OpenStore.Omnichannel.Storefront.Services;

public abstract class BffService : IBffService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    protected BffService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    protected HttpContext HttpContext => _httpContextAccessor.HttpContext;
    protected IDictionary<object, object> HttpContextCache => HttpContext.Items;
    protected ClaimsPrincipal User => HttpContext?.User;
}