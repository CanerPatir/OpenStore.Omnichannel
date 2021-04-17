using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace OpenStore.Omnichannel.Panel.Services
{
    public interface IApiClient
    {
        ProductHttpStore Product { get; }
        Task<bool> Ping();
    }

    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ProductHttpStore Product { get; }

        public ApiClient(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            Product = new ProductHttpStore(httpClient, authenticationStateProvider);
        }

        public async Task<bool> Ping()
        {
            var response = await _httpClient.GetAsync("api/ping");
            return response.IsSuccessStatusCode;
        }
    }
}