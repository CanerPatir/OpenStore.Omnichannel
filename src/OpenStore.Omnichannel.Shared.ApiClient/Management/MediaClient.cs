using System.Net.Http.Json;
using OpenStore.Omnichannel.Shared.Dto.Management;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Shared.ApiClient.Management;

public class MediaClient : BaseClient
{
    public MediaClient(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api/media";

    public async Task<IEnumerable<ProductMediaDto>> UploadProductMedia(IEnumerable<FileUploadDto> fileUploadDtoList)
    {
        var response = await HttpClient.PostAsJsonAsync($"{Path}/product", fileUploadDtoList);
        return await response.Content.ReadFromJsonAsync<List<ProductMediaDto>>();
    }
}