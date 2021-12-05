using MediatR;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Shared.Query.Management.ProductContext;

public record GetProductCollectionItems(Guid ProductCollectionId): IRequest<IEnumerable<ProductCollectionItemDto>>;