using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Application.Extensions;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext
{
    public class ChangeVariantMediaHandler : IRequestHandler<ChangeVariantMedia>
    {
        private readonly ICrudRepository<Product> _repository;

        public ChangeVariantMediaHandler(ICrudRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(ChangeVariantMedia command, CancellationToken cancellationToken)
        {
            var product = await _repository.GetRequired(command.ProductId, cancellationToken);
            product.ChangeVariantMedia(command);
            await _repository.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}