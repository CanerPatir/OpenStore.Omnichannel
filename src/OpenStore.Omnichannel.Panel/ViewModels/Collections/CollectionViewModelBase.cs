using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Panel.ViewModels.Collections;

public abstract class CollectionViewModelBase : BaseViewModel
{
    private bool _saving;
    private ProductCollectionDto _collection;

    public bool Saving
    {
        get => _saving;
        protected set => SetValue(ref _saving, value);
    }

    public ProductCollectionDto Collection
    {
        get => _collection;
        protected set => SetValue(ref _collection, value);
    }

    public void ChangeCollectionMeta(string html, string description)
    {
        Collection.Description = html;

        if (Collection.IsCreate)
        {
            Collection.Handle = new SlugHelper().GenerateSlug(Collection.Title);
            Collection.MetaTitle = Collection.Title;
            Collection.MetaDescription = description;
        }
 
        OnPropertyChanged();
    }
}