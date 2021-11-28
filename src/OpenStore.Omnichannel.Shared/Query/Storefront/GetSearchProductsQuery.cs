using MediatR;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.Shared.Query.Storefront;

public record GetSearchProductsQuery(string Term, int BatchSize, int FirstIndex): IRequest<SearchProductsResult>;