using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Omnichannel.ReadModel.Query;
using OpenStore.Omnichannel.ReadModel.Query.Result;

namespace OpenStore.Omnichannel.ReadModel.Sql
{
    public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, GetProductDetailResult>
    {
        public Task<GetProductDetailResult> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}