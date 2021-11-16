using MediatR;
using OpenStore.Omnichannel.Shared.Query.Result;

namespace OpenStore.Omnichannel.Shared.Query;

public record GetSearchProductsQuery(string Term, int BatchSize, int FirstIndex): IRequest<SearchProductsResult>;