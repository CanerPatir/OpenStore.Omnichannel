using Microsoft.AspNetCore.Components;
using OpenStore.Omnichannel.Panel.Services;

namespace OpenStore.Omnichannel.Panel.ViewModels.Collections;

public class CollectionUpdateViewModel : CollectionViewModelBase
{
 
    
    private readonly IApiClient _apiClient;
    private readonly NavigationManager _navigationManager;
 
 
    public CollectionUpdateViewModel(IApiClient apiClient, NavigationManager navigationManager)
    {
        _apiClient = apiClient;
        _navigationManager = navigationManager;
    }
    
    public async Task Retrieve(Guid id)
    {
        Collection = await _apiClient.Collection.Get(id);
    }
    
}