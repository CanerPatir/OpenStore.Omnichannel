using MediatR;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.Shared.Query.Storefront;

public record GetProductDetailByHandleQuery(string Handle) : IRequest<ProductDetailResult>;
 