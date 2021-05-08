using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command
{
    public class DeleteProductHandler : IRequestHandler<DeleteProduct>
    {
        private readonly ICrudRepository<Product> _repository;

        public DeleteProductHandler(ICrudRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteProduct request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(request.Id, cancellationToken);
            product.Delete();
            await _repository.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}