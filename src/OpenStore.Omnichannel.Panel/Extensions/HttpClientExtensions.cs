using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using OpenStore.Omnichannel.Panel.Services;

// ReSharper disable CheckNamespace

namespace OpenStore.Omnichannel.Panel
{

    public static class HttpClientExtensions
    {
        public static async Task<PagedList<T>> GetPage<T>(this HttpClient httpClient, string path, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var qs = $"?pageNumber={pageNumber}&pageSize={pageSize}";
            var response = await httpClient.GetAsync(path + qs, cancellationToken);
            await response.HandleError(cancellationToken);

            return await response.Content.ReadFromJsonAsync<PagedList<T>>(cancellationToken: cancellationToken);
        }

        public static async Task HandleError(this HttpResponseMessage response, CancellationToken cancellationToken = default)
        {
            if (!response.IsSuccessStatusCode)
            {
                var isInvalidToken = false;
                var authHeaderExists = response.Headers.TryGetValues("WWW-Authenticate", out var values);

                if (authHeaderExists)
                {
                    isInvalidToken = values.Any(x => x.Contains("invalid_token"));
                }

                ErrorReadModel errorReadModel = null;
                string rawErrorMessage = null;

                try
                {
                    errorReadModel = await response.Content.ReadFromJsonAsync<ErrorReadModel>(cancellationToken: cancellationToken);
                }
                catch (Exception e)
                {
                    try
                    {
                        rawErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                    }
                    catch (Exception e2)
                    {
                        // ignored
                    }
                    // ignored
                }

                throw new ApiException(response.StatusCode,
                    errorReadModel,
                    rawErrorMessage,
                    isInvalidToken
                );
            }
        }
    }
}