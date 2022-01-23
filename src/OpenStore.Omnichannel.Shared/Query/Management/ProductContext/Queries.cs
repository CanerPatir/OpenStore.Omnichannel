using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Shared.Query.Management.ProductContext;


public record GetAllCollectionsQuery(PageRequest PageRequest) : IQuery<PagedList<CollectionListItemDto>>;

public record GetAllProductsQuery(PageRequest PageRequest, ProductStatus? Status) : IQuery<PagedList<ProductListItemDto>>;

public record GetProductCollectionEligibleItemsQuery(Guid ProductCollectionId, string Term) : IQuery<IEnumerable<ProductCollectionItemDto>>;

public record GetProductCollectionForUpdateQuery(Guid Id) : IQuery<ProductCollectionDto>;

public record GetProductCollectionItemsQuery(Guid ProductCollectionId): IQuery<IEnumerable<ProductCollectionItemDto>>;

public record GetProductForUpdateQuery(Guid Id) : IQuery<ProductDto>;