using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Application.Extensions;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Application.Query.ProductContext;

public record GetAllProducts(PageRequest PageRequest, ProductStatus? Status) : IRequest<PagedList<ProductListItemDto>>;

public class GetAllProductsHandler : IRequestHandler<GetAllProducts, PagedList<ProductListItemDto>>
{
    private readonly ICrudRepository<Product> _repository;

    public GetAllProductsHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    public Task<PagedList<ProductListItemDto>> Handle(GetAllProducts request, CancellationToken cancellationToken)
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