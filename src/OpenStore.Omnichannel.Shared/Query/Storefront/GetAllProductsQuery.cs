using MediatR;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.Shared.Query.Storefront;

public record GetAllProductsQuery(int BatchSize, int FirstIndex) : IRequest<AllProductsResult>;
 