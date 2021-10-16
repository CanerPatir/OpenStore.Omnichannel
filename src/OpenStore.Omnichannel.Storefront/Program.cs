using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using OpenStore.Omnichannel.Storefront.Infrastructure;
using OpenStore.Omnichannel.Storefront.Services;
using OpenStore.Omnichannel.Storefront.Services.Clients;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
// Add services to the container.
services.Scan(x => x.FromAssemblyOf<IViewModelFactory>().AddClasses(c => c.AssignableTo<IViewModelFactory>()).AsSelf().WithSingletonLifetime());

services.AddControllersWithViews();
services.AddAntiforgery();
services.AddResponseCompression();

var identityConfiguration = builder.Configuration.GetSection("IdentityConfiguration").Get<IdentityConfiguration>();

services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.Cookie.Name = "OpenStore";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(identityConfiguration.SessionExpireTimeInMinutes);
        options.SlidingExpiration = true;
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
    })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.Authority = identityConfiguration.Authority;
        options.ClientId = "OpenStore.Web";
        options.ClientSecret = "secret";

        options.SaveTokens = true;
        options.RequireHttpsMetadata = true;
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;

        options.GetClaimsFromUserInfoEndpoint = true;
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.Scope.Add("roles");
        options.Scope.Add("offline_access");

        options.SecurityTokenValidator = new JwtSecurityTokenHandler
        {
            // Disable the built-in JWT claims mapping feature.
            InboundClaimTypeMap = new Dictionary<string, string>()
        };

        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";

        options.AccessDeniedPath = "/";
    });

var apiConfiguration = builder.Configuration.GetSection("Api").Get<ApiConfiguration>();
services.AddHttpClient(ApiClient.apiClientKey, client =>
{
    client.BaseAddress = new Uri(apiConfiguration.Url);
    client.Timeout = TimeSpan.FromMilliseconds(apiConfiguration.TimeoutMilliseconds);
});

services.AddSingleton<IApiClient, ApiClient>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();