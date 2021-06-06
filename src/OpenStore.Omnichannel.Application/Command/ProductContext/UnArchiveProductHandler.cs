using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext
{
    public class UnArchiveProductHandler : IRequestHandler<UnArchiveProduct>
    {
        private readonly ICrudRepository<Product> _repository;

        public UnArchiveProductHandler(ICrudRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UnArchiveProduct command, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(command.Id, cancellationToken);
            product.UnArchive();
            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}