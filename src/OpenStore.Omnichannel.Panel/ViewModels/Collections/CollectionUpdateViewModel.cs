using Microsoft.AspNetCore.Components;
using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.Dto.Management;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Panel.ViewModels.Collections;

public class CollectionUpdateViewModel : CollectionViewModelBase
{
    private readonly IApiClient _apiClient;
    private readonly NavigationManager _navigationManager;
    private bool _deleting;
    private List<ProductCollectionItemDto> _collectionItems = new();
    private List<ProductCollectionItemDto> _filteredCollectionItems = new List<ProductCollectionItemDto>();

    public CollectionUpdateViewModel(IApiClient apiClient, NavigationManager navigationManager)
    {
        _apiClient = apiClient;
        _navigationManager = navigationManager;
    }

    public bool HasImage => Collection.Media is not null;
    
    private Guid Id => Collection.Id.Value;
    
    public bool Deleting
    {
        get => _deleting;
        private set => SetValue(ref _deleting, value);
    }

    public List<ProductCollectionItemDto> CollectionItems
    {
        get => _collectionItems;
        private set => SetValue(ref _collectionItems, value);
    }
    
    public List<ProductCollectionItemDto> FilteredCollectionItems
    {
        get => _filteredCollectionItems;
        private set => SetValue(ref _filteredCollectionItems, value);
    }

    private IEnumerable<ProductCollectionItemDto> SelectedCollectionItems => CollectionItems.Where(x => x.Selected);

    public async Task Retrieve(Guid id)
    {
        Collection = await _apiClient.Collection.Get(id);
        CollectionItems = (await _apiClient.Collection.GetItems(Id)).ToList();
        FilteredCollectionItems = CollectionItems.ToList();
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

    public void SelectAllItems(bool isChecked)
    {
        foreach (var item in CollectionItems)
        {
            item.Selected = isChecked;
        }
    }

    public void SearchInItems(string term)
    {
        FilteredCollectionItems = CollectionItems.Where(x => x.Title.Contains(term, StringComparison.CurrentCultureIgnoreCase)).ToList();
    }

    public async Task BulkRemoveItems()
    {
        foreach (var item in SelectedCollectionItems)
        {
            await _apiClient.Collection.RemoveItem(Id, item.ProductId);
            CollectionItems.Remove(item);
            FilteredCollectionItems.Remove(item);
        }
    }
    
    public async Task RemoveItem(ProductCollectionItemDto item)
    {
        await _apiClient.Collection.RemoveItem(Id, item.ProductId);
        CollectionItems.Remove(item);
        FilteredCollectionItems.Remove(item);
    }
}