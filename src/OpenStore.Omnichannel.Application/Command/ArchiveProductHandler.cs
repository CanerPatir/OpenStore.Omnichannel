using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command
{
    public class ArchiveProductHandler : IRequestHandler<ArchiveProduct>
    {
        private readonly ICrudRepository<Product> _repository;

        public ArchiveProductHandler(ICrudRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(ArchiveProduct request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(request.Id, cancellationToken);
            product.Archive();
            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}