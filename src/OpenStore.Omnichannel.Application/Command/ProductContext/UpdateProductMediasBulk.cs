using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UpdateProductMediasBulkHandler : ICommandHandler<UpdateProductMedias>
{
    private readonly ICrudRepository<Product> _repository;

    public UpdateProductMediasBulkHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateProductMedias command, CancellationToken cancellationToken)
    {
        var product = await _repository.Query
            .Include(x => x.Medias)
            .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
        product.UpdateProductMedias(command);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}