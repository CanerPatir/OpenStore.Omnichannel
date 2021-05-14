using System.Threading.Tasks;
using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.ReadModel;

namespace OpenStore.Omnichannel.Panel.ViewModels
{
    public class ProductIndexViewModel : BaseViewModel
    {
        private readonly IApiClient _apiClient;

        public ProductIndexViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<PagedListDto<ProductListItemReadModel>> GetAll(PageRequest pageRequest) => await _apiClient.Product.GetAll(pageRequest);
        public async Task<PagedListDto<ProductListItemReadModel>> GetActive(PageRequest pageRequest) => await _apiClient.Product.GetActive(pageRequest);
        public async Task<PagedListDto<ProductListItemReadModel>> GetDraft(PageRequest pageRequest) => await _apiClient.Product.GetDraft(pageRequest);
        public async Task<PagedListDto<ProductListItemReadModel>> GetArchived(PageRequest pageRequest) => await _apiClient.Product.GetArchived(pageRequest);
    }
}