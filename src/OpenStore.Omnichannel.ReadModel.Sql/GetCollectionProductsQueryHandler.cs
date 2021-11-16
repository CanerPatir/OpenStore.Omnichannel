using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Query;
using OpenStore.Omnichannel.Shared.Query.Result;

namespace OpenStore.Omnichannel.ReadModel.Sql;

public class GetCollectionProductsQueryHandler: IRequestHandler<GetAllProductsQuery, AllProductsResult>
{
    private readonly ICrudRepository<Product> _repository;

    public GetCollectionProductsQueryHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<AllProductsResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(10, cancellationToken);
        return null;
    }
}