using System.Net.Http.Json;
using OpenStore.Omnichannel.Shared.Dto.Management;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.Request;
using OpenStore.Omnichannel.Shared.Request.ProductContext;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Panel.Services;

public class ProductHttpStore : HttpStore
{
    protected override string Path => "api/products";

    public ProductHttpStore(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Guid> Create(ProductDto model)
    {
        var response = await HttpClient.PostAsJsonAsync($"{Path}", model);
        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    public Task Update(Guid productId, ProductDto model) => HttpClient.PutAsJsonAsync($"{Path}/{productId}", model);

    public Task<ProductDto> Get(Guid productId) => HttpClient.GetFromJsonAsync<ProductDto>($"{Path}/{productId}");

    public Task<PagedList<ProductListItemDto>> GetAll(PageRequest request) => HttpClient.GetPage<ProductListItemDto>($"{Path}/all", request);

    public Task<PagedList<ProductListItemDto>> GetActive(PageRequest request) => HttpClient.GetPage<ProductListItemDto>($"{Path}/active", request);

    public Task<PagedList<ProductListItemDto>> GetDraft(PageRequest request) => HttpClient.GetPage<ProductListItemDto>($"{Path}/draft", request);

    public Task<PagedList<ProductListItemDto>> GetArchived(PageRequest request) => HttpClient.GetPage<ProductListItemDto>($"{Path}/archived", request);

    public Task Archive(Guid productId) => HttpClient.PostAsync($"{Path}/{productId}/archive", null!);

    public Task UnArchive(Guid productId) => HttpClient.PostAsync($"{Path}/{productId}/un-archive", null!);

    public async Task<IEnumerable<ProductMediaDto>> AssignProductMedia(Guid productId, List<FileUploadDto> fileUploadDtoList)
    {
        var response = await HttpClient.PostAsJsonAsync($"{Path}/{productId}/assign-media", fileUploadDtoList);
        return await response.Content.ReadFromJsonAsync<List<ProductMediaDto>>();
    }

    public Task UpdateProductMedias(Guid productId, IEnumerable<ProductMediaDto> medias) => HttpClient.PostAsJsonAsync($"{Path}/{productId}/update-medias", medias);

    public Task DeleteProductMedia(Guid productId, Guid productMediaId) => HttpClient.DeleteAsync($"{Path}/{productId}/medias/{productMediaId}");

    public Task UpdateVariantPrices(Guid productId, UpdateVariantPricesRequest request) => HttpClient.PostAsJsonAsync($"{Path}/{productId}/variants/update-prices", request);

    public Task UpdateVariantStocks(Guid productId, UpdateVariantStocksRequest request) => HttpClient.PostAsJsonAsync($"{Path}/{productId}/variants/update-stocks", request);

    public Task UpdateVariantBarcodes(Guid productId, UpdateVariantBarcodesRequest request) =>
        HttpClient.PostAsJsonAsync($"{Path}/{productId}/variants/update-barcodes", request);

    public Task UpdateVariantSkus(Guid productId, UpdateVariantSkusRequest request) => HttpClient.PostAsJsonAsync($"{Path}/{productId}/variants/update-skus", request);

    public async Task<Guid> CreateVariant(Guid productId, VariantDto model)
    {
        var response = await HttpClient.PostAsJsonAsync($"{Path}/{productId}/variants", model);
        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    public Task UpdateVariant(Guid productId, Guid variantId, VariantDto model) => HttpClient.PutAsJsonAsync($"{Path}/{productId}/variants/{variantId}", model);

    public Task DeleteVariants(Guid productId, IEnumerable<Guid> model) => HttpClient.PostAsJsonAsync($"{Path}/{productId}/variants/delete-bulk", model);

    public Task DeleteVariant(Guid productId, Guid variantId) => DeleteVariants(productId, new[] { variantId });

    public async Task<IEnumerable<Guid>> MakeProductAsMultiVariantRequest(Guid productId, MakeProductAsMultiVariantRequest request)
    {
        var response = await HttpClient.PostAsJsonAsync($"{Path}/{productId}/make-multi-variant", request);
        return await response.Content.ReadFromJsonAsync<List<Guid>>();
    }

    public Task SaveVariantMedia(Guid productId, Guid variantId, Guid mediaId) =>
        HttpClient.PostAsync($"{Path}/{productId}/variants/{variantId}/change-variant-media/{mediaId}", null);
}