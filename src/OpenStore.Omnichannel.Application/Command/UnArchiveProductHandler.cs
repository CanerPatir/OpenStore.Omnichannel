using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command
{
    public class UnArchiveProductHandler : IRequestHandler<UnArchiveProduct>
    {
        private readonly ICrudRepository<Product> _repository;

        public UnArchiveProductHandler(ICrudRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UnArchiveProduct request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(request.Id, cancellationToken);
            product.UnArchive();
            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}