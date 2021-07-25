using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext
{
    public class UpdateProductVariantHandler : IRequestHandler<UpdateProductVariant>
    {
        private readonly ICrudRepository<Product> _repository;

        public UpdateProductVariantHandler(ICrudRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateProductVariant command, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(command.ProductId, cancellationToken);
            product.UpdateVariant(command);
            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}