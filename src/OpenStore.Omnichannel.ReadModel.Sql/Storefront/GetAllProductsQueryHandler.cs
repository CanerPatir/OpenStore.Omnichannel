using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Data.EntityFramework.ReadOnly;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Query.Storefront;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.ReadModel.Sql.Storefront;

public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, AllProductsQueryResult>
{
    private readonly IReadOnlyRepository<Product> _repository;

    public GetAllProductsQueryHandler(IReadOnlyRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<AllProductsQueryResult> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        var (batchSize, firstIndex) = query;

        var products = await _repository.Query
            .Include(x => x.Variants)
            .Include(x => x.Medias)
            .Skip(firstIndex)
            .Take(batchSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new AllProductsQueryResult(products.Select(x => new ProductItemDto(
            x.Id,
            x.Title,
            x.FirstMedia?.Url,
            x.Handle,
            x.Variants.First().Price,
            x.Variants.First().CompareAtPrice
        )).ToList());
    }
}