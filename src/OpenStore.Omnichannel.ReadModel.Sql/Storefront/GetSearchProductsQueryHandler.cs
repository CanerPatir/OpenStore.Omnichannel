using MediatR;
using OpenStore.Omnichannel.Shared.Query.Storefront;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.ReadModel.Sql.Storefront;

public class GetSearchProductsQueryHandler : IRequestHandler<GetSearchProductsQuery, SearchProductsResult>
{
    public Task<SearchProductsResult> Handle(GetSearchProductsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}