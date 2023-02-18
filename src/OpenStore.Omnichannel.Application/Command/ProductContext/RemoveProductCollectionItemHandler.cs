using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Application.Exceptions;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class RemoveProductCollectionItemHandler : ICommandHandler<RemoveProductCollectionItem>
{
    private readonly ICrudRepository<ProductCollection> _repository;

    public RemoveProductCollectionItemHandler(ICrudRepository<ProductCollection> repository)
    {
        _repository = repository;
    }

    public async Task Handle(RemoveProductCollectionItem command, CancellationToken cancellationToken)
    {
        var (productCollectionId, productId) = command;
        var productCollection = await _repository.Query
            .Include(x => x.ProductItems)
            .SingleOrDefaultAsync(x => x.Id == productCollectionId, cancellationToken);

        if (productCollection is null) throw new ResourceNotFoundException();

        productCollection.RemoveItem(command);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}