using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Application.Exceptions;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.Query.Management.ProductContext;

namespace OpenStore.Omnichannel.ReadModel.Sql.Management.ProductContext;

public class GetProductCollectionForUpdateHandler : IRequestHandler<GetProductCollectionForUpdate, ProductCollectionDto>
{
    private readonly ICrudRepository<ProductCollection> _repository;
    private readonly IOpenStoreObjectMapper _mapper;

    public GetProductCollectionForUpdateHandler(ICrudRepository<ProductCollection> repository, IOpenStoreObjectMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductCollectionDto> Handle(GetProductCollectionForUpdate request, CancellationToken cancellationToken)
    {
        var product = await _repository.Query
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null)
        {
            throw new ResourceNotFoundException();
        }

        return _mapper.Map<ProductCollectionDto>(product);
    }
}