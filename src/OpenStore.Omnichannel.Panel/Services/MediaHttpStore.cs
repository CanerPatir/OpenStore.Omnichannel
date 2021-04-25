using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using OpenStore.Omnichannel.Shared.Dto;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Panel.Services
{
    public class MediaHttpStore : HttpStore
    {
        public MediaHttpStore(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider) : base(httpClient, authenticationStateProvider)
        {
        }

        protected override string Path => "api/media";

        public async Task<IEnumerable<ProductMediaDto>> UploadProductMedia(IEnumerable<FileUploadDto> fileUploadDtoList)
        {
            var response = await HttpClient.PostAsJsonAsync($"{Path}/product", fileUploadDtoList);
            await response.HandleError();
            return await response.Content.ReadFromJsonAsync<List<ProductMediaDto>>();
        }
    }
}