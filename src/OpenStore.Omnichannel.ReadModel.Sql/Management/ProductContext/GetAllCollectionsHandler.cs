using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.Query.Management.ProductContext;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.ReadModel.Sql.Management.ProductContext;

public class GetAllCollectionsHandler : IRequestHandler<GetAllCollections, PagedList<CollectionListItemDto>>
{
    private readonly ICrudRepository<ProductCollection> _repository;

    public GetAllCollectionsHandler(ICrudRepository<ProductCollection> repository)
    {
        _repository = repository;
    }
    
    public Task<PagedList<CollectionListItemDto>> Handle(GetAllCollections query, CancellationToken cancellationToken)
    {
        var pageRequest = query.PageRequest;
        var q = _repository.Query;

        if (!string.IsNullOrWhiteSpace(pageRequest.FilterTerm))
        {
            q = q.Where(x => x.Title.Contains(pageRequest.FilterTerm));
        }

        return q
            .GetPaged(
                pageRequest.PageNumber,
                pageRequest.PageSize,
                p => new CollectionListItemDto(
                    p.Id,
                    p.Title,
                    p.Media?.Url
                ),
                cancellationToken
            );
    }
}