using Microsoft.Extensions.DependencyInjection;

namespace OpenStore.Omnichannel.Shared.HttpClient.Management;

public static class ServiceCollectionExtensions
{
    private const string ClientName = "main";

    public static IHttpClientBuilder AddManagementApiClient(this IServiceCollection services, Uri clientBaseAddress)
    {
      
        services.AddSingleton<IApiClient, ApiClient>(sp =>
        {
            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient(ClientName);
            return new ApiClient(httpClient);
        });
        
        return services
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
           
            ;

        
    }
}