using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Panel.ViewModels
{
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

        public async Task Create(string description)
        {
            Saving = true;
            try
            {
                Product.Description = description;
                var productId = await ApiClient.Product.CreateProduct(Product);
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
}