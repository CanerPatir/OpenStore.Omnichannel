using OpenIddict.Validation.AspNetCore;
using OpenStore.Infrastructure.Localization;
using OpenStore.Infrastructure.Logging;
using OpenStore.Infrastructure.Web.ErrorHandling;
using OpenStore.Infrastructure.Web.Modularization;
using OpenStore.Infrastructure.Web.Swagger;
using OpenStore.Omnichannel;
using OpenStore.Omnichannel.Api.Storefront;
using OpenStore.Omnichannel.Infrastructure;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Seeders;

const string allowAllCorsPolicy = nameof(allowAllCorsPolicy);


var builder = WebApplication.CreateBuilder(args)
    .AddOpenStoreLogging();

var services = builder.Services;
// Add services to the container.

var mvcBuilder = services.AddControllersForAssemblies();
services.AddResponseCompression();
services.AddInfrastructure(mvcBuilder, builder.Environment, builder.Configuration, withScheduledJobs: true);

services.AddCors(o => o.AddPolicy(allowAllCorsPolicy, builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

if (builder.Environment.IsDevelopment())
{
    services.AddOpenStoreSwaggerForModule<CartController>(builder.Environment, "OpenStore.Omnichannel");
}

services.AddAuthentication(options => { options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme; });
// Register the OpenIddict validation components.
services
    .AddOpenIddict()
    .AddValidation(options =>
    {
        // Note: the validation handler uses OpenID Connect discovery
        // to retrieve the address of the introspection endpoint.
        options.SetIssuer(builder.Configuration.GetIdentityConfiguration().Authority.Trim('/'));
        options.AddAudiences("OpenStore.Omnichannel");

        // Configure the validation handler to use introspection and register the client
        // credentials used when communicating with the remote introspection endpoint.
        options.UseIntrospection()
            .SetClientId("OpenStore.Omnichannel")
            .SetClientSecret("b935acea-99bb-442a-ba40-3223d4bb9d4c");

        // options.AddEncryptionKey(new SymmetricSecurityKey(Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));

        // Register the System.Net.Http integration.
        options.UseSystemNetHttp();

        // Register the ASP.NET Core host.
        options.UseAspNetCore();

        // For applications that need immediate access token or authorization
        // revocation, the database entry of the received tokens and their
        // associated authorizations can be validated for each API call.
        // Enabling these options may have a negative impact on performance.
        //
        // options.EnableAuthorizationEntryValidation();
        // options.EnableTokenEntryValidation();
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenStoreSwaggerForModule("OpenStore.Omnichannel");
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
}

if (ForceHttps())
{
    app.UseHttpsRedirection();
}

app.UseOpenStoreApiErrorHandling();
app.UseOpenStoreLocalization();

app
    .UseCors(allowAllCorsPolicy)
    .UseStaticFiles()
    .UseResponseCompression()
    .UseSubApplication<OpenStore.Omnichannel.Identity.Startup>("/identity")
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints => { endpoints.MapControllers(); });

DataSeeder.SeedAsync(app.Services).Wait();

app.Run();

bool ForceHttps() => builder.Configuration.GetValue<bool>("HttpsRedirection");
