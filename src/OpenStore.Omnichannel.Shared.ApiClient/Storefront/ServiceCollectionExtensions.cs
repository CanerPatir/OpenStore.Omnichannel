using Microsoft.Extensions.DependencyInjection;

namespace OpenStore.Omnichannel.Shared.ApiClient.Storefront;

public static class ServiceCollectionExtensions
{
 
    public static IHttpClientBuilder AddStorefrontApiClient(this IServiceCollection services, Uri clientBaseAddress, TimeSpan timeout)
    {
        services.AddSingleton<IStorefrontApiClient, StorefrontApiClient>();

      return  services.AddHttpClient(StorefrontApiClient.ClientName, client =>
            {
                client.BaseAddress = clientBaseAddress;
                client.Timeout = timeout;
            })
            .AddPolicyHandler(StorefrontApiRetryPolicy.GetApiRetryPolicy())
            ;
    }
}