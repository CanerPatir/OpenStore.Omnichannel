using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.Server;
using OpenStore.Omnichannel.Domain.IdentityContext;

namespace OpenStore.Omnichannel.Identity.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class OidcConfigurationController : Controller
{
    private readonly OpenIddictApplicationManager<ApplicationClient> _applicationManager;
    private readonly OpenIddictServerOptions _openIdDictServerOptions;

    public OidcConfigurationController(
        OpenIddictApplicationManager<ApplicationClient> applicationManager,
        IOptions<OpenIddictServerOptions> openIdDictServerOptions)
    {
        _applicationManager = applicationManager;
        _openIdDictServerOptions = openIdDictServerOptions.Value;
    }

    [HttpGet("_configuration/{clientId}")]
    public async Task<IActionResult> GetClientRequestParameters([FromRoute] string clientId)
    {
        var client = await _applicationManager.FindByClientIdAsync(clientId);
        var redirectUris = JsonSerializer.Deserialize<string[]>(client.RedirectUris);
        var postLogoutRedirectUris = JsonSerializer.Deserialize<string[]>(client.PostLogoutRedirectUris);
        var scopes = JsonSerializer.Deserialize<string[]>(client.Permissions).Where(x => x.StartsWith(OpenIddictConstants.Permissions.Prefixes.Scope))
            .Select(x => x.Split(":")[1]);

        return Ok(new Dictionary<string, string>
        {
            ["authority"] = GetIssuer(_openIdDictServerOptions),
            ["client_id"] = client.ClientId,
            ["redirect_uri"] = redirectUris.First(),
            ["post_logout_redirect_uri"] = postLogoutRedirectUris.First(),
            ["response_type"] = "code",
            ["scope"] = OpenIddictConstants.Scopes.OpenId + " " + string.Join(" ", scopes)
        });
    }

    public string GetIssuer(OpenIddictServerOptions options)
    {
        var issuer = options.Issuer;
        if (issuer == null)
        {
            if (!Uri.TryCreate(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host +
                               HttpContext.Request.PathBase, UriKind.Absolute, out issuer))
            {
                throw new InvalidOperationException("The issuer address cannot be inferred from the current request");
            }
        }

        return issuer.AbsoluteUri;
    }
}