using OpenStore.Omnichannel.Shared.Query.Storefront;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.ReadModel.Sql.Storefront;

public class GetSearchProductsQueryHandler : IQueryHandler<GetSearchProductsQuery, SearchProductsQueryResult>
{
    public Task<SearchProductsQueryResult> Handle(GetSearchProductsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}