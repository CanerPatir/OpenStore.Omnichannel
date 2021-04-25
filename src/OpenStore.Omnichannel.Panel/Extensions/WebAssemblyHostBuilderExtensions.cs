using System;
using System.Net.Http;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Blazorise.RichTextEdit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenStore.Omnichannel.Panel.Services;

namespace OpenStore.Omnichannel.Panel.Extensions
{
    public static class WebAssemblyHostBuilderExtensions
    {
        private const string ClientName = "main";

        public static WebAssemblyHostBuilder AddServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddLocalization(opts => opts.ResourcesPath = "Resources");
            builder.Services
                .AddBlazorise(options => { options.ChangeTextOnKeyPress = true; })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons()
                .AddBlazoriseRichTextEdit(options => { options.DynamicLoadReferences = true; })
                ;

            var environment = builder.HostEnvironment;

            var clientBaseAddress = new Uri(new Uri(builder.Configuration.GetValue<string>("ApiGateway")).GetLeftPart(UriPartial.Authority));
            builder.Services
                .AddHttpClient(ClientName)
                .ConfigureHttpClient((sp, client) => { client.BaseAddress = clientBaseAddress; })
                // .AddTransientHttpErrorPolicy(p =>
                // {
                //     return p.WaitAndRetryAsync(new[]
                //     {
                //         TimeSpan.FromSeconds(1),
                //         TimeSpan.FromSeconds(4)
                //     });
                // })
                .AddHttpMessageHandler(_ => new TokenErrorMessageHandler())
                .AddHttpMessageHandler(sp =>
                {
                    var navigationManager = sp.GetRequiredService<NavigationManager>();
                    var handler = new AuthorizationMessageHandler(sp.GetRequiredService<IAccessTokenProvider>(), navigationManager);
                    return handler.ConfigureHandler(new[] {clientBaseAddress.ToString()});
                })
                ;

            builder.Services.AddTransient<IApiClient, ApiClient>(sp =>
            {
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(ClientName);
                return new ApiClient(
                    httpClient,
                    sp.GetRequiredService<AuthenticationStateProvider>()
                );
            });


            return builder;
        }
    }
}