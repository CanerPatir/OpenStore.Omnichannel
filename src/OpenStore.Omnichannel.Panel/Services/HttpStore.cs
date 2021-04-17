using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace OpenStore.Omnichannel.Panel.Services
{
    public abstract class HttpStore
    {
        protected HttpStore(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            HttpClient = httpClient;
            AuthenticationStateProvider = authenticationStateProvider;
        }

        protected HttpClient HttpClient { get; }
        protected AuthenticationStateProvider AuthenticationStateProvider { get; }

        protected abstract string Path { get; }

        protected virtual string GetPath(object route) => $"{Path}/{route}";

        protected async Task<Guid> GetUserId()
        {
            if (AuthenticationStateProvider == null)
            {
                throw new ArgumentNullException(nameof(AuthenticationStateProvider));
            }
            var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            return authenticationState.User.GetId();
        }
    }
}