using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OpenStore.Omnichannel.Shared.Dto.Management;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.ApiClient.Management;

namespace OpenStore.Omnichannel.Panel.ViewModels.Products;

public class UpdateVariantViewModel : BaseViewModel
{
    private readonly IApiClient _apiClient;
    private readonly NavigationManager _navigationManager;

    private bool _deleting;
    private bool _saving;
    private bool _savingVariantImage;
    private ProductDto _product;
    private VariantDto _model;
    private List<MediaEditorItemModel> _mediaEditorItems = new();

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

    public virtual List<MediaEditorItemModel> MediaEditorItems
    {
        get => _mediaEditorItems;
        protected set => SetValue(ref _mediaEditorItems, value);
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

    public bool SavingVariantImage
    {
        get => _savingVariantImage;
        private set => SetValue(ref _savingVariantImage, value);
    }

    public bool IsNull => Model is null;

    // ReSharper disable once PossibleInvalidOperationException
    public Guid ProductId => Product.Id.Value;

    // ReSharper disable once PossibleInvalidOperationException
    // ReSharper disable once MemberCanBePrivate.Global
    public Guid VariantId => Model.Id.Value;

    public string DisplayImageUrl => Product.Medias.OrderBy(x => x.Position).FirstOrDefault()?.Url;

    public ProductMediaDto VariantDisplayImage => GetVariantDisplayImage(Model);

    public bool ModelDisplayImageUrlExists => !string.IsNullOrWhiteSpace(VariantDisplayImage?.Url);
    public MediaEditorItemModel SelectedMediaEditorItem => MediaEditorItems.SingleOrDefault(x => x.Selected);

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

    public ProductMediaDto GetVariantDisplayImage(VariantDto variant)
    {
        if (variant == null)
        {
            return null;
        }

        return Product?.Medias?.FirstOrDefault(x => x.VariantIds.Contains(variant.Id.Value));
    }

    #region VariantMediaEditor

    public async Task UploadNewMedia(IBrowserFile file, long maxAllowed)
    {
        var dtoList = new List<FileUploadDto>();
        var index = Product.Medias.Count();
        var bytes = new byte[file.Size];
        await using var openReadStream = file.OpenReadStream(maxAllowed);
        await openReadStream.ReadAsync(bytes);
        dtoList.Add(new FileUploadDto(file.Name, file.ContentType, file.Size, index++, bytes));

        var mediaDtoList = await _apiClient.Product.AssignProductMedia(ProductId, dtoList);

        var model = new List<ProductMediaDto>();
        model.AddRange(Product.Medias);
        model.AddRange(mediaDtoList);

        Product.Medias = model;
        MediaEditorItems = Product.Medias.Select(x => new MediaEditorItemModel
        {
            Dto = x
        }).OrderBy(x => x.Dto.Position).ToList();
    }

    public void InitMediaEditorItems()
    {
        MediaEditorItems = Product.Medias.Select(x => new MediaEditorItemModel
        {
            Dto = x,
        }).OrderBy(x => x.Dto.Position).ToList();
        ClearVariantMediaSelection();
    }

    public void SelectVariantMedia(MediaEditorItemModel selected)
    {
        foreach (var mediaEditorItemModel in MediaEditorItems.Where(i => i != selected))
        {
            mediaEditorItemModel.Selected = false;
        }
    }

    public void ClearVariantMediaSelection()
    {
        foreach (var mediaEditorItemModel in MediaEditorItems)
        {
            mediaEditorItemModel.Selected = mediaEditorItemModel.Dto == VariantDisplayImage;
        }
    }

    public async Task SaveVariantMedia()
    {
        if (SelectedMediaEditorItem.Dto == VariantDisplayImage) return;

        SavingVariantImage = true;
        try
        {
            var mediaId = SelectedMediaEditorItem.Dto.Id;
            await _apiClient.Product.SaveVariantMedia(ProductId, VariantId, mediaId);
            foreach (var mediaDto in Product.Medias.Where(x => x.VariantIds.Contains(VariantId)))
            {
                mediaDto.VariantIds.Remove(VariantId);
            }

            var productMediaDto = Product.Medias?.FirstOrDefault(x => x.Id == mediaId);
            productMediaDto?.VariantIds.Add(VariantId);
        }
        finally
        {
            SavingVariantImage = false;
        }
    }

    #endregion
}