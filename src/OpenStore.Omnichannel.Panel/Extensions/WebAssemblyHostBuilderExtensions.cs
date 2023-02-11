using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Blazorise.RichTextEdit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Localization;
using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Panel.ViewModels.Collections;
using OpenStore.Omnichannel.Panel.ViewModels.Inventory;
using OpenStore.Omnichannel.Panel.ViewModels.Orders;
using OpenStore.Omnichannel.Panel.ViewModels.Products;
using OpenStore.Omnichannel.Panel.ViewModels.StoreManagement;
using OpenStore.Omnichannel.Shared.HttpClient.Management;

namespace OpenStore.Omnichannel.Panel.Extensions;

public static class WebAssemblyHostBuilderExtensions
{

    public static WebAssemblyHostBuilder AddServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddLocalization(opts => opts.ResourcesPath = "Resources");
        builder.Services
            .AddBlazorise(options => { options.Immediate = true; })
            .AddBootstrapProviders()
            .AddFontAwesomeIcons()
            .AddBlazoriseRichTextEdit(options => { options.DynamicallyLoadReferences = true; })
            ;

        var environment = builder.HostEnvironment;

        var clientBaseAddress = new Uri(new Uri(builder.Configuration.GetValue<string>("ApiGateway")).GetLeftPart(UriPartial.Authority));
        builder.Services
            .AddManagementApiClient(clientBaseAddress)
            .AddHttpMessageHandler(sp =>
            {
                var navigationManager = sp.GetRequiredService<NavigationManager>();
                var alertService = sp.GetRequiredService<DialogService>();
                var sharedLocalizer = sp.GetRequiredService<IStringLocalizer<App>>();
                return new HttpErrorMessageHandler(navigationManager, alertService, sharedLocalizer);
            })
            .AddHttpMessageHandler(sp =>
            {
                var navigationManager = sp.GetRequiredService<NavigationManager>();
                var handler = new AuthorizationMessageHandler(sp.GetRequiredService<IAccessTokenProvider>(), navigationManager);
                return handler.ConfigureHandler(new[] { clientBaseAddress.ToString() });
            })
            ;
        
        builder.Services.AddSingleton<DialogService>();
        builder.Services.AddSingleton<ProductCreateViewModel>();
        builder.Services.AddSingleton<ProductUpdateViewModel>();
        builder.Services.AddSingleton<ProductIndexViewModel>();
        builder.Services.AddSingleton<UpdateVariantViewModel>();
        builder.Services.AddSingleton<PreferencesViewModel>();
        builder.Services.AddSingleton<InventoryIndexViewModel>();
        builder.Services.AddSingleton<CollectionIndexViewModel>();
        builder.Services.AddSingleton<CollectionCreateViewModel>();
        builder.Services.AddSingleton<CollectionUpdateViewModel>();
        builder.Services.AddSingleton<OrderIndexViewModel>();
        builder.Services.AddSingleton<OrderCreateViewModel>();
        builder.Services.AddSingleton<OrderUpdateViewModel>();

        return builder;
    }
}