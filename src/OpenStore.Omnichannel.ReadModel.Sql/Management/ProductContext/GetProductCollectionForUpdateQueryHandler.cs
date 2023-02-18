using Microsoft.EntityFrameworkCore;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Application.Exceptions;
using OpenStore.Data.EntityFramework.ReadOnly;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.Query.Management.ProductContext;

namespace OpenStore.Omnichannel.ReadModel.Sql.Management.ProductContext;

public class GetProductCollectionForUpdateQueryHandler : IQueryHandler<GetProductCollectionForUpdateQuery, ProductCollectionDto>
{
    private readonly IReadOnlyRepository<ProductCollection> _repository;
    private readonly IOpenStoreObjectMapper _mapper;

    public GetProductCollectionForUpdateQueryHandler(IReadOnlyRepository<ProductCollection> repository, IOpenStoreObjectMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductCollectionDto> Handle(GetProductCollectionForUpdateQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.Query
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null)
        {
            throw new ResourceNotFoundException();
        }

        return _mapper.Map<ProductCollectionDto>(product);
    }
}