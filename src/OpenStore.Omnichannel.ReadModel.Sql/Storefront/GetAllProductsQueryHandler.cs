using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Query.Storefront;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.ReadModel.Sql.Storefront;

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
            .Include(x => x.Variants)
            .Include(x => x.Medias)
            .Skip(firstIndex)
            .Take(batchSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new AllProductsResult(products.Select(x => new ProductItemDto(
            x.Id,
            x.Title,
            x.FirstMedia?.Url,
            x.Handle,
            x.Variants.First().Price,
            x.Variants.First().CompareAtPrice
        )).ToList());
    }
}