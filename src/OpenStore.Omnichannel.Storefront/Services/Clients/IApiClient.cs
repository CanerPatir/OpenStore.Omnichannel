namespace OpenStore.Omnichannel.Storefront.Services.Clients;

public interface IApiClient
{
    CatalogStore Catalog { get; }
    ShoppingCartStore ShoppingCart { get; }
}