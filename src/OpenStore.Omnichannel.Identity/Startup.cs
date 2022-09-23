using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenStore.Data.EntityFramework.Seed;
using OpenStore.Infrastructure.Localization;
using OpenStore.Infrastructure.Mapping.AutoMapper;
using OpenStore.Infrastructure.Web.ErrorHandling;
using OpenStore.Infrastructure.Web.Modularization;
using OpenStore.Infrastructure.Web.Swagger;
using OpenStore.Omnichannel.Domain.IdentityContext;
using OpenStore.Omnichannel.Identity.Controllers.Api;
using OpenStore.Omnichannel.Infrastructure;
using OpenStore.Omnichannel.Infrastructure.Data.EntityFramework.Context;
using Quartz;

namespace OpenStore.Omnichannel.Identity;

public class Startup : ModuleStartup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
    {
    }

    private const string AllowAllCorsPolicy = nameof(AllowAllCorsPolicy);

    // This method gets called by the runtime. Use this method to add services to the container.
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(o => o.AddPolicy(AllowAllCorsPolicy, builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));

        var mvcBuilder = services
            .AddControllersWithViewsForAssemblies(Assembly)
            .AddViewLocalization()
            .AddDataAnnotationsLocalization();

        services
            .Configure<RouteOptions>(options => { options.LowercaseUrls = true; })
            .AddOpenStoreObjectMapper(mc => { })
            // .AddOpenStoreRecurringJob<IdentityBackgroundService>(IdentityBackgroundService.EveryHour)
            .AddInfrastructure(mvcBuilder, Environment, Configuration);

        // services.AddDbContext<ApplicationDbContext>(options =>
        // {
        //     // Configure the context to use Microsoft SQL Server.
        //     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        //
        //     // Register the entity sets needed by OpenIddict.
        //     // Note: use the generic overload if you need
        //     // to replace the default OpenIddict entities.
        //     options.UseOpenIddict();
        // });

        // Configure Identity to use the same JWT claims as OpenIddict instead
        // of the legacy WS-Federation claims it uses by default (ClaimTypes),
        // which saves you from doing the mapping in your authorization controller.
        // services.Configure<IdentityOptions>(options =>
        // {
        //     options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
        //     options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
        //     options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
        // });
        // done in infrastructure
        
        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
        });
        // services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        if (Environment.IsDevelopment())
        {
            services.AddOpenStoreSwaggerForModule<UsersController>(Environment, "identity");
        }
        services.AddOpenIddict()

            // Register the OpenIddict core components.
            .AddCore(options =>
            {
                // Configure OpenIddict to use the Entity Framework Core stores and models.
                options.UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>()
                    .ReplaceDefaultEntities<ApplicationClient,
                        ApplicationAuthorization,
                        ApplicationScope,
                        ApplicationToken, Guid>();
                options.UseQuartz();
            })

            // Register the OpenIddict server components.
            .AddServer(options =>
            {
                // Enable the authorization, logout, token and userinfo endpoints.
                options.SetAuthorizationEndpointUris("/connect/authorize")
                    // .SetDeviceEndpointUris("/connect/device")
                    .SetLogoutEndpointUris("/connect/logout")
                    .SetTokenEndpointUris("/connect/token")
                    .SetUserinfoEndpointUris("/connect/userinfo")
                    .SetIntrospectionEndpointUris("/connect/introspect")
                    .SetVerificationEndpointUris("/connect/verify");

                // Note: the Mvc.Client sample only uses the code flow and the password flow, but you
                // can enable the other flows if you need to support implicit or client credentials.
                options
                    .AllowAuthorizationCodeFlow()
                    // .AllowDeviceCodeFlow()
                    // .AllowPasswordFlow()
                    .AllowRefreshTokenFlow()
                    .AllowImplicitFlow()
                    ;

                // Mark the "email", "profile", "roles" scopes as supported scopes.
                options.RegisterScopes(
                    OpenIddictConstants.Scopes.Profile,
                    OpenIddictConstants.Scopes.Roles,
                    OpenIddictConstants.Scopes.Email
                );

                // warn: dont use prod
                // Register the signing and encryption credentials.
                // options
                //     .AddDevelopmentEncryptionCertificate()
                //     .AddDevelopmentSigningCertificate()
                //     ;
                // options.AddEphemeralSigningKey(); // for development not use in production
                // options.AddEphemeralEncryptionKey();

                options.AddEncryptionKey(new SymmetricSecurityKey(Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));
                // options.AddSigningKey(new SymmetricSecurityKey(Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));
                // options.AddSigningCertificate(GetCert());
                options.AddDevelopmentSigningCertificate();

                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options.UseAspNetCore()
                    .EnableStatusCodePagesIntegration()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableTokenEndpointPassthrough()
                    .EnableUserinfoEndpointPassthrough()
                    .EnableVerificationEndpointPassthrough()
                    .DisableTransportSecurityRequirement(); // During development, you can disable the HTTPS requirement.

                // Note: if you don't want to specify a client_id when sending
                // a token or revocation request, uncomment the following line:
                //
                // options.AcceptAnonymousClients();

                // Note: if you want to process authorization and token requests
                // that specify non-registered scopes, uncomment the following line:
                //
                // options.DisableScopeValidation();

                // Note: if you don't want to use permissions, you can disable
                // permission enforcement by uncommenting the following lines:
                //
                // options.IgnoreEndpointPermissions()
                //        .IgnoreGrantTypePermissions()
                //        .IgnoreScopePermissions();

                // Note: when issuing access tokens used by third-party APIs
                // you don't own, you can disable access token encryption:
                //
                // options.DisableAccessTokenEncryption();
            })

            // Register the OpenIddict validation components.
            .AddValidation(options =>
            {
                // Configure the audience accepted by this resource server.
                // The value MUST match the audience associated with the
                // "demo_api" scope, which is used by ResourceController.
                // options.AddAudiences("OnlineCourse.Api");

                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();

                // For applications that need immediate access token or authorization
                // revocation, the database entry of the received tokens and their
                // associated authorizations can be validated for each API call.
                // Enabling these options may have a negative impact on performance.
                //
                options.EnableAuthorizationEntryValidation();
                options.EnableTokenEntryValidation();
            });

        if (Environment.IsDevelopment())
        {
            IdentityModelEventSource.ShowPII = true;
        }
    }

    private X509Certificate2 GetCert()
    {
        using var resFilestream = Assembly.GetManifestResourceStream("OnlineCourse.Identity.Resources.Cert.pfx");
        if (resFilestream == null) return null;
        var bytes = new byte[resFilestream.Length];
        resFilestream.Read(bytes, 0, bytes.Length);
        return new X509Certificate2(bytes, "0", X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
    }

    public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (IsSeparatedService)
        {
            app.UseHttpsRedirection();
        }

        app.UseOpenStoreMvcErrorHandling("/error");
        app.UseCors(AllowAllCorsPolicy);
        app.UseOpenStoreLocalization();

        if (Environment.IsDevelopment())
        {
            app.UseOpenStoreSwaggerForModule("identity", "identity");
        }
        
        app
            .UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new EmbeddedFileProvider(
                    Assembly,
                    $"{Assembly.GetName().Name}.wwwroot"
                )
            })
            .UseOpenStoreLocalization()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); })
            ;
    }

    protected override async Task OnStarting(IServiceProvider serviceProvider, CancellationToken cancellationToken) => await Migrate(serviceProvider, cancellationToken);

    private static async Task Migrate(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        await DataSeeder<ApplicationDbContext>.Create(scope.ServiceProvider)
            .Seed((_, provider, token) => new IdentityDataSeeder(provider).Seed(token), cancellationToken);
    }
}