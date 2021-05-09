using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OpenStore.Omnichannel.Shared.Dto.Product;
using OpenStore.Omnichannel.Shared.ReadModel;

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

        public Task<PagedListDto<ProductListItemReadModel>> GetAll(PageRequest request) => HttpClient.GetPage<ProductListItemReadModel>($"{Path}/all", request);

        public Task<PagedListDto<ProductListItemReadModel>> GetActive(PageRequest request) => HttpClient.GetPage<ProductListItemReadModel>($"{Path}/active", request);

        public Task<PagedListDto<ProductListItemReadModel>> GetDraft(PageRequest request) => HttpClient.GetPage<ProductListItemReadModel>($"{Path}/draft", request);

        public Task<PagedListDto<ProductListItemReadModel>> GetArchived(PageRequest request) => HttpClient.GetPage<ProductListItemReadModel>($"{Path}/archived", request);

        public async Task Archive(ProductDto productDto)
        {
            await HttpClient.PostAsync($"{Path}/{productDto.Id.Value}/archive", null!);
            productDto.Status = ProductStatus.Archived;
        }

        public async Task UnArchive(ProductDto productDto)
        {
            await HttpClient.PostAsync($"{Path}/{productDto.Id.Value}/un-archive", null!);
            productDto.Status = ProductStatus.Active;
        }
    }
}