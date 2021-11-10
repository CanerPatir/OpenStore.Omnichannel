using MediatR;
using OpenStore.Omnichannel.Shared.Query;
using OpenStore.Omnichannel.Shared.Query.Result;

namespace OpenStore.Omnichannel.ReadModel.Sql;

public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, ProductDetailResult>
{
    public Task<ProductDetailResult> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}