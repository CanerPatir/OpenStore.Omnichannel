using MediatR;
using OpenStore.Omnichannel.Shared.Query.Result;

namespace OpenStore.Omnichannel.Shared.Query;

public record GetAllProductsQuery(int BatchSize, int FirstIndex) : IRequest<AllProductsResult>;
 