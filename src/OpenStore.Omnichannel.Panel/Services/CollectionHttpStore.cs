using System.Net.Http.Json;
using OpenStore.Omnichannel.Shared.Dto.Management;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Panel.Services;

public class CollectionHttpStore : HttpStore
{
    public CollectionHttpStore(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api/collections";

    public Task<PagedList<CollectionListItemDto>> GetAll(PageRequest request) => HttpClient.GetPage<CollectionListItemDto>(Path, request);

    public async Task<Guid> Create(ProductCollectionDto model)
    {
        var response = await HttpClient.PostAsJsonAsync(Path, model);
        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    public Task<ProductCollectionDto> Get(Guid id) => HttpClient.GetFromJsonAsync<ProductCollectionDto>($"{Path}/{id}");

    public Task Update(Guid id, ProductCollectionDto model) => HttpClient.PutAsJsonAsync($"{Path}/{id}", model);
    
    public async Task Delete(Guid id)
    {
        var response = await HttpClient.DeleteAsync($"{Path}/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<ProductCollectionMediaDto> UpdateImage(Guid id, FileUploadDto dto)
    {
        var response = await HttpClient.PostAsJsonAsync($"{Path}/{id}/change-image", dto);
        return await response.Content.ReadFromJsonAsync<ProductCollectionMediaDto>();
    }

    public async Task RemoveImage(Guid id)
    {
        var response = await HttpClient.PostAsync($"{Path}/{id}/remove-image", null);
        response.EnsureSuccessStatusCode();
    }
}