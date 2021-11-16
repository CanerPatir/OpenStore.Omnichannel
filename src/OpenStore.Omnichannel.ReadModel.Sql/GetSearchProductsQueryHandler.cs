using MediatR;
using OpenStore.Omnichannel.Shared.Query;
using OpenStore.Omnichannel.Shared.Query.Result;

namespace OpenStore.Omnichannel.ReadModel.Sql;

public class GetSearchProductsQueryHandler : IRequestHandler<GetSearchProductsQuery, SearchProductsResult>
{
    public Task<SearchProductsResult> Handle(GetSearchProductsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}