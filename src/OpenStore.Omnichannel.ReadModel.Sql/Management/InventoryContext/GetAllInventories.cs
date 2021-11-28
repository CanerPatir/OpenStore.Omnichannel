using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Inventory;
using OpenStore.Omnichannel.Shared.Query.Management.InventoryContext;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.ReadModel.Sql.Management.InventoryContext;

public class GetAllInventoriesHandler : IRequestHandler<GetAllInventories, PagedList<InventoryListItemDto>>
{
    private readonly ICrudRepository<Variant> _repository;

    public GetAllInventoriesHandler(ICrudRepository<Variant> repository)
    {
        _repository = repository;
    }

    public Task<PagedList<InventoryListItemDto>> Handle(GetAllInventories request, CancellationToken cancellationToken)
    {
        var pageRequest = request.PageRequest;

        IQueryable<Variant> q = _repository.Query
            .Include(x => x.Inventory)
            .Include(x => x.Product);

        if (!string.IsNullOrWhiteSpace(pageRequest.FilterTerm))
        {
            var term = pageRequest.FilterTerm;
            q = q
                    .Where(x => x.Product.Title.Contains(term) || x.Sku.Contains(term) || x.Barcode.Contains(term))
                ;
        }

        q = q.OrderBy(x => x.ProductId);

        return q
            .GetPaged(
                pageRequest.PageNumber,
                pageRequest.PageSize,
                v => new InventoryListItemDto(
                    v.Inventory.Id,
                    v.ProductId,
                    v.Product.Title,
                    v.Product.FirstMedia?.Url,
                    v.Sku,
                    v.Option1,
                    v.Option2,
                    v.Option3,
                    v.Inventory.ContinueSellingWhenOutOfStock,
                    v.Inventory.Quantity,
                    v.Inventory.AvailableQuantity
                ),
                cancellationToken
            );
    }
}