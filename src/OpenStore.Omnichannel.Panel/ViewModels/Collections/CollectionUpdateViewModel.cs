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
    private bool _savingEligibleItems;
    private bool _searchingForEligibleItems;
    private List<ProductCollectionItemDto> _collectionItems = new();
    private List<ProductCollectionItemDto> _filteredCollectionItems = new();
    private List<ProductCollectionItemDto> _eligibleCollectionItems = new();

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

    public bool SavingEligibleItems
    {
        get => _savingEligibleItems;
        private set => SetValue(ref _savingEligibleItems, value);
    }

    public bool SearchingForEligibleItems
    {
        get => _searchingForEligibleItems;
        private set => SetValue(ref _searchingForEligibleItems, value);
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

    public IEnumerable<ProductCollectionItemDto> SelectedCollectionItems => CollectionItems.Where(x => x.Selected);

    public List<ProductCollectionItemDto> EligibleCollectionItems
    {
        get => _eligibleCollectionItems;
        private set => SetValue(ref _eligibleCollectionItems, value);
    }

    public IEnumerable<ProductCollectionItemDto> SelectedEligibleCollectionItems => EligibleCollectionItems.Where(x => x.Selected);

    public async Task Retrieve(Guid id)
    {
        Collection = await _apiClient.Collection.Get(id);
        await RetrieveCollectionItems();
    }

    private async Task RetrieveCollectionItems()
    {
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

    #region Items

    #endregion

    public void SelectAllItems(bool isChecked)
    {
        foreach (var item in CollectionItems) item.Selected = isChecked;
    }

    public void SearchInItems(string term)
    {
        FilteredCollectionItems = CollectionItems
            .Where(x => x.Title.Contains(term, StringComparison.CurrentCultureIgnoreCase))
            .ToList();
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

    public async Task SearchForEligibleItems(string term)
    {  if (string.IsNullOrWhiteSpace(term))
        {
            EligibleCollectionItems = new List<ProductCollectionItemDto>();
            return;
        }

        SearchingForEligibleItems = true;
        try
        {
            EligibleCollectionItems = (await _apiClient.Collection.SearchForEligibleItems(Id, term)).ToList();
        }
        finally
        {
            SearchingForEligibleItems = false;
        }
    }

    public async Task SaveSelectedEligibleItems()
    {
        SavingEligibleItems = true;
        try
        {
            await _apiClient.Collection.AddItemsToCollection(Id, SelectedEligibleCollectionItems.Select(x => x.ProductId));
            await RetrieveCollectionItems();
        }
        finally
        {
            SavingEligibleItems = false;
        }
    }
}