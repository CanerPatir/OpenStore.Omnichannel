using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Application.Exceptions;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Application.Query.ProductContext
{
    public record GetProductForUpdate(Guid Id) : IRequest<ProductDto>;

    public class GetProductForUpdateHandler : IRequestHandler<GetProductForUpdate, ProductDto>
    {
        private readonly ICrudRepository<Product> _repository;
        private readonly IOpenStoreObjectMapper _mapper;

        public GetProductForUpdateHandler(ICrudRepository<Product> repository, IOpenStoreObjectMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductForUpdate request, CancellationToken cancellationToken)
        {
            var product = await _repository.Query
                .Include(x => x.Medias)
                .Include(x => x.Variants)
                .ThenInclude(v => v.Inventory)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (product is null)
            {
                throw new ResourceNotFoundException();
            }

            return _mapper.Map<ProductDto>(product);
        }
    }
}