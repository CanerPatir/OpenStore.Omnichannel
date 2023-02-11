using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using OpenStore.Infrastructure.Localization;
using OpenStore.Infrastructure.Localization.Json;
using OpenStore.Omnichannel.Shared.HttpClient.Storefront;
using OpenStore.Omnichannel.Storefront.Infrastructure;
using OpenStore.Omnichannel.Storefront.Services;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddHttpContextAccessor();
// Add services to the container.
services.Scan(x => x.FromAssemblyOf<IBffService>().AddClasses(c => c.AssignableTo<IBffService>()).AsSelf().WithScopedLifetime());

var mvcBuilder = services.AddControllersWithViews();

if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

builder.Services.AddOpenStoreJsonLocalization(mvcBuilder, options =>
{
    options.Source = OpenStoreJsonLocalizationSource.Content;
    options.DefaultUiCulture = new CultureInfo("tr-TR");
    options.DefaultSupportedUiCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("tr-TR")
    };
});

// builder.Services.AddOpenStoreJsonLocalization(mvcBuilder, options =>
// {
//     options.DefaultUiCulture = new CultureInfo("tr-TR");
//     options.DefaultSupportedUiCultures = new[]
//     {
//         new CultureInfo("en-US"),
//         new CultureInfo("tr-TR")
//     };
//     options.Source = OpenStoreJsonLocalizationSource.Content;
// });

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

        options.SaveTokens = true;
    });

var apiConfiguration = builder.Configuration.GetSection("Api").Get<ApiConfiguration>();

services.AddStorefrontApiClient(new Uri(apiConfiguration.Url), TimeSpan.FromMilliseconds(apiConfiguration.TimeoutMilliseconds))
    .AddHttpMessageHandler(sp => new AuthenticateHttpClientHandler(sp.GetRequiredService<IHttpContextAccessor>()));

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

app.UseHttpsRedirection();
app.UseOpenStoreLocalization();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();