using OpenStore.Omnichannel.Panel.Services;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Panel.ViewModels.Products;

public abstract class ProductViewModelBase : BaseViewModel
{
    private ProductDto _product;
    private bool _archiving;
    private bool _unArchiving;

    public virtual ProductDto Product
    {
        get => _product;
        protected set => SetValue(ref _product, value);
    }

    public bool Archiving
    {
        get => _archiving;
        private set => SetValue(ref _archiving, value);
    }

    public bool UnArchiving
    {
        get => _unArchiving;
        private set => SetValue(ref _unArchiving, value);
    }

    public bool IsNull => Product is null;

    protected IApiClient ApiClient { get; }

    protected ProductViewModelBase(IApiClient apiClient)
    {
        ApiClient = apiClient;
    }

    public async Task Archive()
    {
        Archiving = true;
        try
        {
            await ApiClient.Product.Archive(Product.Id.Value);
            Product.Status = ProductStatus.Archived;
        }
        finally
        {
            Archiving = false;
        }
    }

    public async Task UnArchive()
    {
        UnArchiving = true;
        try
        {
            await ApiClient.Product.UnArchive(Product.Id.Value);
            Product.Status = ProductStatus.Active;
        }
        finally
        {
            UnArchiving = false;
        }
    }

    public void ChangeProductMeta(string html, string description)
    {
        Product.Description = html;

        if (Product.IsCreate)
        {
            Product.Handle = new SlugHelper().GenerateSlug(Product.Title);
            Product.MetaTitle = Product.Title;
            Product.MetaDescription = description;
        }


        OnPropertyChanged();
    }
}