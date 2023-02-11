using Microsoft.Extensions.DependencyInjection;

namespace OpenStore.Omnichannel.Shared.HttpClient.Storefront;

public static class ServiceCollectionExtensions
{
 
    public static IHttpClientBuilder AddStorefrontApiClient(this IServiceCollection services, Uri clientBaseAddress, TimeSpan timeout)
    {
        services.AddSingleton<IStorefrontApiClient, StorefrontApiClient>();

      return  services.AddHttpClient(StorefrontApiClient.ApiClientKey, client =>
            {
                client.BaseAddress = clientBaseAddress;
                client.Timeout = timeout;
            })
            .AddPolicyHandler(RetryPolicy.GetApiRetryPolicy())
            ;
    }
}