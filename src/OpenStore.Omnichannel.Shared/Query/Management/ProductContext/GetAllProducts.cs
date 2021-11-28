using MediatR;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Shared.Query.Management.ProductContext;

public record GetAllProducts(PageRequest PageRequest, ProductStatus? Status) : IRequest<PagedList<ProductListItemDto>>;