namespace OpenStore.Omnichannel.Storefront.Models.Components
{
    public class SearchBoxViewModel
    {
        public SearchBoxViewModel(string term)
        {
            Term = term;
        }

        public string Term { get; }
    }
}