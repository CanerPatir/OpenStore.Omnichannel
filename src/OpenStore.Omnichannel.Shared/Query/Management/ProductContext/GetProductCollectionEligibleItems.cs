using MediatR;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Shared.Query.Management.ProductContext;

public record GetProductCollectionEligibleItems(Guid ProductCollectionId, string Term) : IRequest<IEnumerable<ProductCollectionItemDto>>;