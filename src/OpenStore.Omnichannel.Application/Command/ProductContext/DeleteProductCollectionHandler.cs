using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class DeleteProductCollectionHandler : AsyncRequestHandler<DeleteProductCollection>
{
    private readonly ICrudRepository<ProductCollection> _repository;

    public DeleteProductCollectionHandler(ICrudRepository<ProductCollection> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(DeleteProductCollection command, CancellationToken cancellationToken)
    {
        await _repository.RemoveByIdAsync(command.ProductCollectionId, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}