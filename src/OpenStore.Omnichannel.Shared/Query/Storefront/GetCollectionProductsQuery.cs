using MediatR;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.Shared.Query.Storefront;

public record GetCollectionProductsQuery(Guid CollectionId, int BatchSize, int FirstIndex) : IRequest<CollectionProductsResult>;
