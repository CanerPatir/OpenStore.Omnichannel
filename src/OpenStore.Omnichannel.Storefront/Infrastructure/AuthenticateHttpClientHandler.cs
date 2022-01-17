using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using OpenIddict.Abstractions;

namespace OpenStore.Omnichannel.Storefront.Infrastructure;

public class AuthenticateHttpClientHandler : DelegatingHandler
{
    private const string TokenName = "access_token";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticateHttpClientHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        var accessToken = await httpContext.GetTokenAsync(TokenName);
        // string idToken = await HttpContext.GetTokenAsync("id_token"); 
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode != HttpStatusCode.Unauthorized)
            return response;

        
        accessToken = await RenewToken(httpContext);
        if (accessToken is null)
             return response;
        
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        response = await base.SendAsync(request, cancellationToken);

        return response;
    }

    private static async Task<string> RenewToken(HttpContext httpContext)
    {
        httpContext.Response.Redirect("/login?redirectUri=/checkout");
        await httpContext.Response.CompleteAsync();
        return null;
        
        var authenticateResult = await httpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);
        if (!authenticateResult.Succeeded)
        {
            return null;
        }
        var accessToken = authenticateResult.Properties.GetTokenValue("access_token");
        var refreshToken = authenticateResult.Properties.GetTokenValue("refresh_token");
        return accessToken;
    }
}