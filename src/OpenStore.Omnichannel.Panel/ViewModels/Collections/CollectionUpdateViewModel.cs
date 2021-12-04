using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.Dto.Management;

namespace OpenStore.Omnichannel.Panel.ViewModels.Collections;

public class CollectionUpdateViewModel : CollectionViewModelBase
{
    private readonly IApiClient _apiClient;

    public CollectionUpdateViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public bool HasImage => Collection.Media is not null;

    public async Task Retrieve(Guid id)
    {
        Collection = await _apiClient.Collection.Get(id);
    }

    public async Task Update()
    {
        Saving = true;
        try
        {
            await _apiClient.Collection.Update(Collection.Id.Value, Collection);
        }
        finally
        {
            Saving = false;
        }
    }

    public async Task UpdateImage(FileUploadDto dto)
    {
        var result = await _apiClient.Collection.UpdateImage(Collection.Id.Value, dto);
        Collection.Media = result;
        OnPropertyChanged();
    }

    public async Task RemoveImage()
    {
        await _apiClient.Collection.RemoveImage(Collection.Id.Value);
        Collection.Media = null;
    }
}