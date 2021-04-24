using System.Net.Http;
using Microsoft.AspNetCore.Components.Authorization;

namespace OpenStore.Omnichannel.Panel.Services
{
    public class ProductHttpStore : HttpStore
    { 
        protected override string Path => "api/products";

        public ProductHttpStore(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider) : base(httpClient, authenticationStateProvider)
        {
        }
    }
}