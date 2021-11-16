using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Query;
using OpenStore.Omnichannel.Shared.Query.Result;

namespace OpenStore.Omnichannel.ReadModel.Sql;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, AllProductsResult>
{
    private readonly ICrudRepository<Product> _repository;

    public GetAllProductsQueryHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<AllProductsResult> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        var (batchSize, firstIndex) = query;

        var products = await _repository.Query
            .Skip(firstIndex)
            .Take(batchSize)
            .ToListAsync(cancellationToken);

        return new AllProductsResult(products.Select(x => new ProductItemDto(x.Id,
            x.Title,
            x.FirstMedia?.Url)).ToList());
    }
}