using MediatR;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Shared.Query.Management.ProductContext;

public record GetProductForUpdate(Guid Id) : IRequest<ProductDto>;
