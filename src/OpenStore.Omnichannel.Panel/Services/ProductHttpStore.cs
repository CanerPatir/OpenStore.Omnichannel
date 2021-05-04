using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Panel.Services
{
    public class ProductHttpStore : HttpStore
    {
        protected override string Path => "api/products";

        public ProductHttpStore(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<Guid> CreateProduct(ProductDto model)
        {
            var response = await HttpClient.PostAsJsonAsync($"{Path}", model);
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task<ProductDto> Get(Guid id) => await HttpClient.GetFromJsonAsync<ProductDto>($"{Path}/{id}");
    }
}