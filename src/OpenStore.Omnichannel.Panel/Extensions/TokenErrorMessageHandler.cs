using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace OpenStore.Omnichannel.Panel.Extensions
{
    public class TokenErrorMessageHandler : DelegatingHandler
    {
        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                return base.Send(request, cancellationToken);
            }
            catch (AccessTokenNotAvailableException e)
            {
                e.Redirect();
                throw;
            }
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                return await base.SendAsync(request, cancellationToken);
            }
            catch (AccessTokenNotAvailableException e)
            {
                e.Redirect();
                throw;
            }
        }
    }
}