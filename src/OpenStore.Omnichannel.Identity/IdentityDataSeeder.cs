using NetBox.Extensions;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenStore.Omnichannel.Domain.IdentityContext;

namespace OpenStore.Omnichannel.Identity;

public class IdentityDataSeeder
{
    private readonly OpenIddictApplicationManager<ApplicationClient> _applicationManager;
    private readonly OpenIddictScopeManager<ApplicationScope> _scopeManager;
    private readonly IdentityConfiguration _identityConfig;

    public IdentityDataSeeder(IServiceProvider services)
    {
        _applicationManager = services.GetRequiredService<OpenIddictApplicationManager<ApplicationClient>>();
        _scopeManager = services.GetRequiredService<OpenIddictScopeManager<ApplicationScope>>();
        _identityConfig = services.GetRequiredService<IdentityConfiguration>();
    }

    public async Task Seed(CancellationToken cancellationToken)
    {
        await RegisterApi();
        await RegisterWeb();
        await RegisterPanel();
    }

    private async Task RegisterPanel()
    {
        var openIddictApplicationDescriptor = new OpenIddictApplicationDescriptor
        {
            ClientId = "OpenStore.Omnichannel.Panel",
            ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
            DisplayName = "OpenStore.Omnichannel.Panel",
            Permissions =
            {
                OpenIddictConstants.Permissions.ResponseTypes.IdToken,
                OpenIddictConstants.Permissions.ResponseTypes.IdTokenToken,
                OpenIddictConstants.Permissions.ResponseTypes.Token,
                OpenIddictConstants.Permissions.ResponseTypes.Code,
                OpenIddictConstants.Permissions.Endpoints.Authorization,
                OpenIddictConstants.Permissions.Endpoints.Token,
                OpenIddictConstants.Permissions.Endpoints.Revocation,
                OpenIddictConstants.Permissions.Endpoints.Logout,
                OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                OpenIddictConstants.Permissions.GrantTypes.Implicit,
                OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                OpenIddictConstants.Permissions.Scopes.Email,
                OpenIddictConstants.Permissions.Scopes.Profile,
                OpenIddictConstants.Permissions.Scopes.Roles,
                OpenIddictConstants.Permissions.Prefixes.Scope + "OSScp_API"
            },
            Requirements =
            {
                OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
            },
            ClientType = OpenIddictConstants.ClientTypes.Public
        };


        openIddictApplicationDescriptor.PostLogoutRedirectUris.AddRange(_identityConfig.PanelPostLogoutRedirectUris);
        openIddictApplicationDescriptor.RedirectUris.AddRange(_identityConfig.PanelRedirectUris);


        var found = await _applicationManager.FindByClientIdAsync("OpenStore.Omnichannel.Panel");

        if (found == null)
            await _applicationManager.CreateAsync(openIddictApplicationDescriptor);
        else
            await _applicationManager.UpdateAsync(found, openIddictApplicationDescriptor);
    }

    private async Task RegisterWeb()
    {
        var openIddictApplicationDescriptor = new OpenIddictApplicationDescriptor
        {
            ClientId = "OpenStore.Web",
            ClientSecret = "secret",
            ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
            DisplayName = "OpenStore.Web",
            Permissions =
            {
                OpenIddictConstants.Permissions.ResponseTypes.Code,
                OpenIddictConstants.Scopes.OpenId,
                OpenIddictConstants.Permissions.Endpoints.Authorization,
                OpenIddictConstants.Permissions.Endpoints.Logout,
                OpenIddictConstants.Permissions.Endpoints.Token,
                OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                OpenIddictConstants.Permissions.Scopes.Email,
                OpenIddictConstants.Permissions.Scopes.Profile,
                OpenIddictConstants.Permissions.Scopes.Roles,
                OpenIddictConstants.Permissions.Prefixes.Scope + "OSScp_API"
            },
            Requirements =
            {
                OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
            },
            ClientType = OpenIddictConstants.ClientTypes.Confidential
        };

        openIddictApplicationDescriptor.PostLogoutRedirectUris.AddRange(_identityConfig.WebPostLogoutRedirectUris);
        openIddictApplicationDescriptor.RedirectUris.AddRange(_identityConfig.WebRedirectUris);

        var foundClient = await _applicationManager.FindByClientIdAsync("OpenStore.Web");

        if (foundClient == null)
            await _applicationManager.CreateAsync(openIddictApplicationDescriptor);
        else
            await _applicationManager.UpdateAsync(foundClient, openIddictApplicationDescriptor);
    }

    private async Task RegisterApi()
    {
        if (await _applicationManager.FindByClientIdAsync("OpenStore.Omnichannel") == null)
        {
            var descriptor = new OpenIddictApplicationDescriptor
            {
                ClientId = "OpenStore.Omnichannel",
                ClientSecret = "b935acea-99bb-442a-ba40-3223d4bb9d4c",
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Introspection
                }
            };

            await _applicationManager.CreateAsync(descriptor);
        }

        if (await _scopeManager.FindByNameAsync("OSScp_API") == null)
        {
            var descriptor = new OpenIddictScopeDescriptor
            {
                Name = "OSScp_API",
                Resources =
                {
                    "OpenStore.Omnichannel"
                }
            };

            await _scopeManager.CreateAsync(descriptor);
        }
    }
}