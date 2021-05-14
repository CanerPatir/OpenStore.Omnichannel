using System.Threading.Tasks;
using OpenStore.Omnichannel.Panel.Services;
 
namespace OpenStore.Omnichannel.Panel.ViewModels
{
    public class ProductUpdateViewModel : ProductViewModelBase
    {
        private bool _saving;

        public ProductUpdateViewModel(IApiClient apiClient) : base(apiClient)
        {
        }
        
        public bool Saving
        {
            get => _saving;
            private set => SetValue(ref _saving, value);
        }

        public async Task Update(string description)
        { 
            Saving = true;
            try
            {
                Product.Description = description;
                await ApiClient.Product.UpdateProduct(Product.Id.Value, Product);
            }
            finally
            {
                Saving = false;
            }
        }
    }
}