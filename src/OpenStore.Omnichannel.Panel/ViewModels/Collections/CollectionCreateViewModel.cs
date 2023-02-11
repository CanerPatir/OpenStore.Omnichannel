using Microsoft.AspNetCore.Components;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.HttpClient.Management;

namespace OpenStore.Omnichannel.Panel.ViewModels.Collections;

public class CollectionCreateViewModel : CollectionViewModelBase
{
    private readonly IApiClient _apiClient;
    private readonly NavigationManager _navigationManager;

    public CollectionCreateViewModel(IApiClient apiClient, NavigationManager navigationManager)
    {
        _apiClient = apiClient;
        _navigationManager = navigationManager;
    }

    public void Init()
    {
        Collection = new ProductCollectionDto();
    }

    public async Task Create()
    {
        Saving = true;
        try
        {
            var collectionId = await _apiClient.Collection.Create(Collection);
            _navigationManager.NavigateTo($"products/collections/{collectionId}");
        }
        finally
        {
            Saving = false;
        }
    }
}