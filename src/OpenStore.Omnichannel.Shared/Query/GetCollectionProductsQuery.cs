using MediatR;
using OpenStore.Omnichannel.Shared.Query.Result;

namespace OpenStore.Omnichannel.Shared.Query;

public record GetCollectionProductsQuery(Guid CollectionId, int BatchSize, int FirstIndex) : IRequest<CollectionProductsResult>;
