using Microsoft.AspNetCore.Components;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.ApiClient.Management;

namespace OpenStore.Omnichannel.Panel.ViewModels.Products;

public class ProductCreateViewModel : ProductViewModelBase
{
    private readonly NavigationManager _navigationManager;
    private bool _saving;

    public ProductCreateViewModel(IApiClient apiClient, NavigationManager navigationManager) : base(apiClient)
    {
        _navigationManager = navigationManager;
    }

    public bool Saving
    {
        get => _saving;
        private set => SetValue(ref _saving, value);
    }

    public async Task Create()
    {
        Saving = true;
        try
        {
            var productId = await ApiClient.Product.Create(Product);
            _navigationManager.NavigateTo($"products/{productId}");
        }
        finally
        {
            Saving = false;
        }
    }

    public void Init()
    {
        Product = new ProductDto();
    }
}