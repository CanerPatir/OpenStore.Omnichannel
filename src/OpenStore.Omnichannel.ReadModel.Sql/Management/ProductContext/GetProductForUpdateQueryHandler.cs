using Microsoft.EntityFrameworkCore;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Application.Exceptions;
using OpenStore.Data.EntityFramework.ReadOnly;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.Query.Management.ProductContext;

namespace OpenStore.Omnichannel.ReadModel.Sql.Management.ProductContext;


public class GetProductForUpdateQueryHandler : IQueryHandler<GetProductForUpdateQuery, ProductDto>
{
    private readonly IReadOnlyRepository<Product> _repository;
    private readonly IOpenStoreObjectMapper _mapper;

    public GetProductForUpdateQueryHandler(IReadOnlyRepository<Product> repository, IOpenStoreObjectMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductForUpdateQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.Query
            .Include(x => x.Medias)
            .Include(x => x.Variants)
            .ThenInclude(v => v.Inventory)
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null)
        {
            throw new ResourceNotFoundException();
        }

        return _mapper.Map<ProductDto>(product);
    }
}