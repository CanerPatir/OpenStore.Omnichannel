using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Application.Exceptions;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.Query.Management.ProductContext;

namespace OpenStore.Omnichannel.ReadModel.Sql.Management.ProductContext;

public class GetProductCollectionItemsHandler : IRequestHandler<GetProductCollectionItems, IEnumerable<ProductCollectionItemDto>>
{
    private readonly ICrudRepository<ProductCollection> _repository;

    public GetProductCollectionItemsHandler(ICrudRepository<ProductCollection> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductCollectionItemDto>> Handle(GetProductCollectionItems query, CancellationToken cancellationToken)
    {
        var productCollection = await _repository.Query
            .Include(x => x.ProductItems)
                .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Medias)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == query.ProductCollectionId, cancellationToken);

        if (productCollection is null)
        {
            throw new ResourceNotFoundException();
        }
        
        return productCollection.ProductItems.Select(x => new ProductCollectionItemDto(x.ProductId, x.Product.Title, x.Product.FirstMedia?.Url)).ToList();
    }
}