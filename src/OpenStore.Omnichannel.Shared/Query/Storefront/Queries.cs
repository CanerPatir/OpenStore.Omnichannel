using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.Shared.Query.Storefront;

public record GetAllProductsQuery(int BatchSize, int FirstIndex) : IQuery<AllProductsQueryResult>;

public record GetCollectionProductsQuery(Guid CollectionId, int BatchSize, int FirstIndex) : IQuery<CollectionProductsQueryResult>;

public record GetProductDetailByHandleQuery(string Handle) : IQuery<ProductDetailQueryResult>;

public record GetSearchProductsQuery(string Term, int BatchSize, int FirstIndex): IQuery<SearchProductsQueryResult>;

public record GetShoppingCartQuery(Guid CartId): IQuery<ShoppingCartQueryResult>;
