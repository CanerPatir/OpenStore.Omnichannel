using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command
{
    public class CreateProductHandler : IRequestHandler<CreateProduct, Guid>
    {
        private readonly ICrudRepository<Product> _repository;
        private readonly ICrudRepository<ProductMedia> _productMediaRepository;

        public CreateProductHandler(
            ICrudRepository<Product> repository,
            ICrudRepository<ProductMedia> productMediaRepository
        )
        {
            _repository = repository;
            _productMediaRepository = productMediaRepository;
        }

        public async Task<Guid> Handle(CreateProduct command, CancellationToken cancellationToken)
        {
            var product = Product.Create(command, id => _productMediaRepository.Query.Single(x => x.Id == id));
            await _repository.InsertAsync(product, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}