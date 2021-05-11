using System;
using System.Threading.Tasks;
using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Panel.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private ProductDto _product; 
        public ProductDto Product
        {
            get => _product;
            set => SetValue(ref _product, value);
        }

        private readonly IApiClient _apiClient;

        public ProductViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task Retrieve(Guid id)
        {
            Product = await _apiClient.Product.Get(id);
        }
    }
}