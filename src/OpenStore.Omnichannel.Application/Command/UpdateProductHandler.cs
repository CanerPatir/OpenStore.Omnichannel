using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command
{
    public class UpdateProductHandler : IRequestHandler<UpdateProduct>
    {
        private readonly ICrudRepository<Product> _repository;

        public UpdateProductHandler(ICrudRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateProduct command, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(command.Id, cancellationToken);
            product.UpdatedMasterData(command);
            await _repository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}