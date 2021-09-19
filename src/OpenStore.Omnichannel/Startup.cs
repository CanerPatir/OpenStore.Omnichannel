using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Validation.AspNetCore;
using OpenStore.Infrastructure.Localization;
using OpenStore.Infrastructure.Messaging.Kafka;
using OpenStore.Infrastructure.Web.ErrorHandling;
using OpenStore.Infrastructure.Web.Modularization;
using OpenStore.Infrastructure.Web.Swagger;
using OpenStore.Omnichannel.Infrastructure;
using OpenStore.Omnichannel.ReadModel.Projections;
using OpenStore.Omnichannel.ReadModel.Projections.Consumers;

namespace OpenStore.Omnichannel
{
    public class Startup
    {
        private const string AllowAllCorsPolicy = nameof(AllowAllCorsPolicy);

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddControllersForAssemblies();
            services.AddResponseCompression();
            services.AddInfrastructure(mvcBuilder, Environment, Configuration, withScheduledJobs: true);

            services.AddCors(o => o.AddPolicy(AllowAllCorsPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            if (Environment.IsDevelopment())
            {
                services.AddOpenStoreSwaggerForModule<Startup>(Environment, "OpenStore.Omnichannel");
            }

            services.AddAuthentication(options => { options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme; });
            // Register the OpenIddict validation components.
            services
                .AddOpenIddict()
                .AddValidation(options =>
                {
                    // Note: the validation handler uses OpenID Connect discovery
                    // to retrieve the address of the introspection endpoint.
                    options.SetIssuer(Configuration.GetIdentityConfiguration().Authority.Trim('/'));
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
            
            
            // services.AddKafkaConsumer<Consumer, ProductMessage>("product-events", Configuration.GetSection("Kafka"));
            // services.AddHostedService<TestWorker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseOpenStoreSwaggerForModule("OpenStore.Omnichannel");
            }

            if (env.IsDevelopment())
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
                .UseCors(AllowAllCorsPolicy)
                .UseStaticFiles()
                .UseResponseCompression()
                .UseSubApplication<Identity.Startup>("/identity")
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private bool ForceHttps() => Configuration.GetValue<bool>("HttpsRedirection");
    }
}