using System.Net;
using Polly;
using Polly.Extensions.Http;

namespace OpenStore.Omnichannel.Shared.HttpClient.Storefront;

internal static class RetryPolicy
{
    public static IAsyncPolicy<HttpResponseMessage> GetApiRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}