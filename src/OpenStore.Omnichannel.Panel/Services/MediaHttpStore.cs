using System.Net.Http.Json;
using OpenStore.Omnichannel.Shared.Dto;
using OpenStore.Omnichannel.Shared.Dto.Management;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Panel.Services;

public class MediaHttpStore : HttpStore
{
    public MediaHttpStore(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api/media";

    public async Task<IEnumerable<ProductMediaDto>> UploadProductMedia(IEnumerable<FileUploadDto> fileUploadDtoList)
    {
        var response = await HttpClient.PostAsJsonAsync($"{Path}/product", fileUploadDtoList);
        return await response.Content.ReadFromJsonAsync<List<ProductMediaDto>>();
    }
}