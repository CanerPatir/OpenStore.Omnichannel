using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Data.EntityFramework.ReadOnly;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.Query.Management.ProductContext;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.ReadModel.Sql.Management.ProductContext;


public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, PagedList<ProductListItemDto>>
{
    private readonly IReadOnlyRepository<Product> _repository;

    public GetAllProductsQueryHandler(IReadOnlyRepository<Product> repository)
    {
        _repository = repository;
    }

    public Task<PagedList<ProductListItemDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var (pageRequest, productStatus) = request;

        IQueryable<Product> q = _repository.Query
            .Include(x => x.Medias)
            .Include(x => x.Variants)
            .ThenInclude(x => x.Inventory);

        switch (productStatus)
        {
            case ProductStatus.Active:
                q = q.Where(x => x.Status == ProductStatus.Active);
                break;
            case ProductStatus.Draft:
                q = q.Where(x => x.Status == ProductStatus.Draft);
                break;
            case ProductStatus.Archived:
                q = q.Where(x => x.Status == ProductStatus.Archived);
                break;
            case null:
                q = q.Where(x => x.Status != ProductStatus.Archived);
                break;
        }

        if (!string.IsNullOrWhiteSpace(pageRequest.FilterTerm))
        {
            q = q.Where(x => x.Title.Contains(pageRequest.FilterTerm));
        }

        return q
            .GetPaged(
                pageRequest.PageNumber,
                pageRequest.PageSize,
                p => new ProductListItemDto(
                    p.Id,
                    p.FirstMedia?.Url,
                    p.Status,
                    p.Title,
                    p.Variants.Select(x => x.Inventory).Where(x => x is not null).Sum(x => x.AvailableQuantity),
                    p.HasMultipleVariants,
                    p.Variants.Count,
                    p.IsPhysicalProduct
                ),
                cancellationToken
            );
    }
}