using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.HttpClient.Management;
using OpenStore.Omnichannel.Shared.Request.ProductContext;

namespace OpenStore.Omnichannel.Panel.ViewModels.Products;

public class ProductUpdateViewModel : ProductViewModelBase
{
    private readonly DialogService _dialogService;
    private bool _saving;

    public ProductUpdateViewModel(IApiClient apiClient, DialogService dialogService) : base(apiClient)
    {
        _dialogService = dialogService;
    }

    public bool Saving
    {
        get => _saving;
        private set => SetValue(ref _saving, value);
    }

    public IEnumerable<VariantDto> SelectedVariants => Product.Variants.Where(x => x.Selected);

    // ReSharper disable once PossibleInvalidOperationException
    public Guid ProductId => Product.Id.Value;

    public async Task Retrieve(Guid id)
    {
        Product = await ApiClient.Product.Get(id);
    }

    public async Task Update()
    {
        Saving = true;
        try
        {
            await ApiClient.Product.Update(ProductId, Product);
        }
        finally
        {
            Saving = false;
        }
    }

    public async Task<bool> DeleteSelectedVariants()
    {
        _dialogService.BlockUi();
        try
        {
            var selectedVariants = Product.Variants.Where(x => x.Selected).ToList();

            await ApiClient.Product.DeleteVariants(ProductId, selectedVariants.Select(x => x.Id.Value));

            foreach (var selectedVariant in selectedVariants)
            {
                Product.Variants.Remove(selectedVariant);
            }

            if (!Product.Variants.Any())
            {
                Product.HasMultipleVariants = false;
                Product.Variants.Add(new VariantDto()); // add default variant
                Product.Options.Clear();
                OnPropertyChanged();

                return true;
            }

            OnPropertyChanged();
            return false;
        }
        finally
        {
            _dialogService.UnblockUi();
        }
    }

    public async Task SaveNewVariants()
    {
        _dialogService.BlockUi();
        try
        {
            var ids = (await ApiClient.Product.MakeProductAsMultiVariantRequest(ProductId, new MakeProductAsMultiVariantRequest(Product.Options, Product.Variants))).ToList();
            for (var i = 0; i < ids.Count; i++)
            {
                Product.Variants[i].Id = ids[i];
            }

            Product.HasMultipleVariants = true;
            OnPropertyChanged();
        }
        finally
        {
            _dialogService.UnblockUi();
        }
    }
}