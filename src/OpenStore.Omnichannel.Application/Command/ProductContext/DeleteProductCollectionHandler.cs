using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class DeleteProductCollectionHandler : ICommandHandler<DeleteProductCollection>
{
    private readonly ICrudRepository<ProductCollection> _repository;

    public DeleteProductCollectionHandler(ICrudRepository<ProductCollection> repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteProductCollection command, CancellationToken cancellationToken)
    {
        await _repository.RemoveByIdAsync(command.ProductCollectionId, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}