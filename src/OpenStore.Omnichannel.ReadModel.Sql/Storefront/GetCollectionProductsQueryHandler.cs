using OpenStore.Application.Crud;
using OpenStore.Data.EntityFramework.ReadOnly;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Query.Storefront;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.ReadModel.Sql.Storefront;

public class GetCollectionProductsQueryHandler: IQueryHandler<GetAllProductsQuery, AllProductsQueryResult>
{
    private readonly IReadOnlyRepository<Product> _repository;

    public GetCollectionProductsQueryHandler(IReadOnlyRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<AllProductsQueryResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(10, cancellationToken);
        return null;
    }
}