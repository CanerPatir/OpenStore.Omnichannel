using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.Query.Management.StoreContext.Result;

namespace OpenStore.Omnichannel.Panel.ViewModels.StoreManagement;

public class PreferencesViewModel : BaseViewModel
{
    private readonly IApiClient _apiClient;

    private StorePreferencesQueryResult _storePreferences;
    private bool _isLoading;
    private bool _isSaving;

    public virtual StorePreferencesQueryResult StorePreferences
    {
        get => _storePreferences;
        protected set => SetValue(ref _storePreferences, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        protected set => SetValue(ref _isLoading, value);
    }

    public bool IsSaving
    {
        get => _isSaving;
        protected set => SetValue(ref _isSaving, value);
    }

    public PreferencesViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task Retrieve()
    {
        IsLoading = true;
        try
        {
            StorePreferences = await _apiClient.Store.GetStorePreferences();
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task Save()
    {
        IsSaving = true;
        try
        {
            await _apiClient.Store.UpdateStorePreferences(StorePreferences);
        }
        finally
        {
            IsSaving = false;
        }
    }
}