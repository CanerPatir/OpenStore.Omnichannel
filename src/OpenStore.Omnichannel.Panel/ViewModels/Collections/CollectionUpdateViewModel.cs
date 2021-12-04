using Microsoft.AspNetCore.Components;
using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.Dto.Management;

namespace OpenStore.Omnichannel.Panel.ViewModels.Collections;

public class CollectionUpdateViewModel : CollectionViewModelBase
{
    private readonly IApiClient _apiClient;
    private readonly NavigationManager _navigationManager;
    private bool _deleting;

    public CollectionUpdateViewModel(IApiClient apiClient, NavigationManager navigationManager)
    {
        _apiClient = apiClient;
        _navigationManager = navigationManager;
    }

    public bool HasImage => Collection.Media is not null;
    
    public Guid Id => Collection.Id.Value;

    public bool Deleting
    {
        get => _deleting;
        protected set => SetValue(ref _deleting, value);
    }

    public async Task Retrieve(Guid id)
    {
        Collection = await _apiClient.Collection.Get(id);
    }

    public async Task Update()
    {
        Saving = true;
        try
        {
            await _apiClient.Collection.Update(Id, Collection);
        }
        finally
        {
            Saving = false;
        }
    }

    public async Task UpdateImage(FileUploadDto dto)
    {
        var result = await _apiClient.Collection.UpdateImage(Id, dto);
        Collection.Media = result;
        OnPropertyChanged();
    }

    public async Task RemoveImage()
    {
        await _apiClient.Collection.RemoveImage(Id);
        Collection.Media = null;
    }

    public async Task Delete()
    {
        Deleting = true;
        try
        {
            await _apiClient.Collection.Delete(Id);
            _navigationManager.NavigateTo("products/collections");
        }
        finally
        {
            Deleting = false;
        }
    }
}