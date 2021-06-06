using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext
{
    public class CreateVariantHandler : IRequestHandler<CreateVariant, Guid>
    {
        private readonly ICrudRepository<Product> _repository;
        private readonly ICrudRepository<Variant> _variantRepository;

        public CreateVariantHandler(ICrudRepository<Product> repository, ICrudRepository<Variant> variantRepository)
        {
            _repository = repository;
            _variantRepository = variantRepository;
        }

        public async Task<Guid> Handle(CreateVariant command, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(command.ProductId, cancellationToken);
            
            var variant = product.CreateVariant(command);
            await _variantRepository.InsertAsync(variant, cancellationToken);

            await _repository.SaveChangesAsync(cancellationToken);
            return variant.Id;
        }
    }
}