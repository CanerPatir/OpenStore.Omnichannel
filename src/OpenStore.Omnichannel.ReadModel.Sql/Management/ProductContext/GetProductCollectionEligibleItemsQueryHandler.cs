using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Data.EntityFramework.ReadOnly;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.Query.Management.ProductContext;

namespace OpenStore.Omnichannel.ReadModel.Sql.Management.ProductContext;

public class GetProductCollectionEligibleItemsQueryHandler : IQueryHandler<GetProductCollectionEligibleItemsQuery, IEnumerable<ProductCollectionItemDto>>
{
    private readonly IReadOnlyRepository<ProductCollection> _repository;
    private readonly IMediator _mediator;

    public GetProductCollectionEligibleItemsQueryHandler(IReadOnlyRepository<ProductCollection> repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<IEnumerable<ProductCollectionItemDto>> Handle(GetProductCollectionEligibleItemsQuery query, CancellationToken cancellationToken)
    {
        var (productCollectionId, term) = query;
        var productCollection = await _repository
            .Query
            .Include(x => x.ProductItems)
            .SingleOrDefaultAsync(x => x.Id == productCollectionId, cancellationToken);

        var productList = await _mediator.Send(new GetAllProductsQuery(
            new PageRequest(1, int.MaxValue, null, term, SortDirection.Ascending), null
        ), cancellationToken);

        return productList.Items.Select(x => new ProductCollectionItemDto(
            x.Id,
            x.Title,
            x.PhotoUrl
            )
        {
            Selected = productCollection.ProductItems.Any(i => i.ProductId == x.Id)
        }).ToList();
    }
}