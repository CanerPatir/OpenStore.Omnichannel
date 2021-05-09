using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command
{
    public class UpdateProductMediasBulkHandler : IRequestHandler<UpdateProductMedias>
    {
        private readonly ICrudRepository<Product> _repository;

        public UpdateProductMediasBulkHandler(ICrudRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateProductMedias request, CancellationToken cancellationToken)
        {
            var product = await _repository.Query
                .Include(x => x.Medias)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            product.UpdateProductMedias(request);
            await _repository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}