using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext
{
    public class ArchiveProductHandler : IRequestHandler<ArchiveProduct>
    {
        private readonly ICrudRepository<Product> _repository;

        public ArchiveProductHandler(ICrudRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(ArchiveProduct command, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(command.Id, cancellationToken);
            product.Archive();
            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}