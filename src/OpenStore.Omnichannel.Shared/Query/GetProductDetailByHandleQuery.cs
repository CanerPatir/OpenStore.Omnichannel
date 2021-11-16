using MediatR;
using OpenStore.Omnichannel.Shared.Query.Result;

namespace OpenStore.Omnichannel.Shared.Query;

public record GetProductDetailByHandleQuery(string Handle) : IRequest<ProductDetailResult>;
 