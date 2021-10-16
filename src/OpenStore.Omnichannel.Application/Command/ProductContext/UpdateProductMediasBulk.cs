using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UpdateProductMediasBulkHandler : IRequestHandler<UpdateProductMedias>
{
    private readonly ICrudRepository<Product> _repository;

    public UpdateProductMediasBulkHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateProductMedias command, CancellationToken cancellationToken)
    {
        var product = await _repository.Query
            .Include(x => x.Medias)
            .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
        product.UpdateProductMedias(command);
        await _repository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}