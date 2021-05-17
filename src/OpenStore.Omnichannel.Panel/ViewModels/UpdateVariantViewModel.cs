using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Panel.ViewModels
{
    public class UpdateVariantViewModel : BaseViewModel
    {
        private readonly IApiClient _apiClient;
        private readonly NavigationManager _navigationManager;

        private bool _deleting;
        private bool _saving;
        private ProductDto _product;
        private VariantDto _model;

        public UpdateVariantViewModel(IApiClient apiClient, NavigationManager navigationManager)
        {
            _apiClient = apiClient;
            _navigationManager = navigationManager;
        }

        public virtual ProductDto Product
        {
            get => _product;
            protected set => SetValue(ref _product, value);
        }

        public virtual VariantDto Model
        {
            get => _model;
            protected set => SetValue(ref _model, value);
        }

        public bool Deleting
        {
            get => _deleting;
            private set => SetValue(ref _deleting, value);
        }

        public bool Saving
        {
            get => _saving;
            private set => SetValue(ref _saving, value);
        }

        public bool IsNull => Model is null;

        public Guid ProductId => Product.Id.Value;
        public Guid VariantId => Model.Id.Value;

        public async Task Retrieve(Guid productId, Guid variantId)
        {
            Product = await _apiClient.Product.Get(productId);
            Model = Product.Variants.Single(v => v.Id == variantId);
        }

        public async Task Save()
        {
            Saving = true;
            try
            {
                await _apiClient.Product.UpdateVariant(ProductId, VariantId, Model);
            }
            finally
            {
                Saving = false;
            }
        }

        public async Task Delete()
        {
            Deleting = true;
            try
            {
                await _apiClient.Product.DeleteVariant(ProductId, VariantId);
                _navigationManager.NavigateTo($"/products/{ProductId}");
            }
            finally
            {
                Deleting = false;
            }
        }
    }
}