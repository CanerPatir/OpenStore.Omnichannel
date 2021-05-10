using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command
{
    public class UpdateVariantPricesHandler : IRequestHandler<UpdateVariantPrices>
    {
        private readonly ICrudRepository<Product> _repository;

        public UpdateVariantPricesHandler(ICrudRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateVariantPrices command, CancellationToken cancellationToken)
        {
            var product = await _repository.Query.Include(x => x.Variants).SingleOrDefaultAsync(x => x.Id == command.ProductId, cancellationToken);
            product.UpdateVariantPrices(command);
            await _repository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}