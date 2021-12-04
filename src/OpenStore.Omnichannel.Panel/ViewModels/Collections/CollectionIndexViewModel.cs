using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Panel.ViewModels.Collections;

public class CollectionIndexViewModel : BaseViewModel
{
    private readonly IApiClient _apiClient;

    public CollectionIndexViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    
    public async Task<PagedList<CollectionListItemDto>> GetAll(PageRequest pageRequest) => await _apiClient.Collection.GetAll(pageRequest);

}