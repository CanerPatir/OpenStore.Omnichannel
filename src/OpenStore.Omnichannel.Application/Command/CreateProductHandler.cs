using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command
{
    public class CreateProductHandler : IRequestHandler<CreateProduct, Guid>
    {
        private readonly ICrudRepository<Product> _repository;
        private readonly IOpenStoreObjectMapper _mapper;

        public CreateProductHandler(ICrudRepository<Product> repository, IOpenStoreObjectMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateProduct command, CancellationToken cancellationToken)
        {
            var product = Product.Create(command);
            
            await _repository.InsertAsync(product, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}