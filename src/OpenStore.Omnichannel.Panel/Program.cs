using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using OpenStore.Omnichannel;
using OpenStore.Omnichannel.Panel;
using OpenStore.Omnichannel.Panel.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddServices();

builder.Services.AddOidcAuthentication(options =>
{
    var authority = builder.Configuration.GetValue<string>("IdentityServiceAddress");
    options.ProviderOptions.Authority = authority;
    options.ProviderOptions.ClientId = "OpenStore.Omnichannel.Panel";
    options.ProviderOptions.ResponseType = "code";
    // Note: response_mode=fragment is the best option for a SPA. Unfortunately, the Blazor WASM
    // authentication stack is impacted by a bug that prevents it from correctly extracting
    // authorization error responses (e.g error=access_denied responses) from the URL fragment.
    // For more information about this bug, visit https://github.com/dotnet/aspnetcore/issues/28344.

    options.ProviderOptions.ResponseMode = "query";
    // options.ProviderOptions.ResponseMode = "fragment";
    options.ProviderOptions.DefaultScopes.Add("roles");
    options.ProviderOptions.DefaultScopes.Add("email");
    options.ProviderOptions.DefaultScopes.Add("OSScp_API");
    options.UserOptions.RoleClaim = "role";
    options.AuthenticationPaths.RemoteRegisterPath = authority + "account/register";
});

builder.Services.AddAuthorizationCore(config =>
{
    config.AddPolicy(ApplicationPolicies.AdministratorPolicy, policy => policy.MyRequireRole(ApplicationRoles.Administrator));
});
            
var host = builder.Build();
var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
(jsInterop as IJSInProcessRuntime).InitCulture();
await host.RunAsync();



