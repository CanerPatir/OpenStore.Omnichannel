using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OpenStore.Omnichannel.Shared.Dto;
using OpenStore.Omnichannel.Shared.Dto.Product;
using OpenStore.Omnichannel.Shared.ReadModel;
using OpenStore.Omnichannel.Shared.Request;

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

        public async Task<IEnumerable<ProductMediaDto>> AssignProductMedia(Guid id, List<FileUploadDto> fileUploadDtoList)
        {
            var response = await HttpClient.PostAsJsonAsync($"{Path}/{id}/assign-media", fileUploadDtoList);
            return await response.Content.ReadFromJsonAsync<List<ProductMediaDto>>();
        }

        public Task UpdateProductMedias(Guid id, IEnumerable<ProductMediaDto> medias) => HttpClient.PostAsJsonAsync($"{Path}/{id}/update-medias", medias);
        
        public Task DeleteProductMedia(Guid id, Guid productMediaId) => HttpClient.DeleteAsync($"{Path}/{id}/medias/{productMediaId}");
       
        public Task UpdateVariantPrices(Guid id, UpdateVariantPricesRequest request) => HttpClient.PostAsJsonAsync($"{Path}/{id}/variants/update-prices", request);
    }
}