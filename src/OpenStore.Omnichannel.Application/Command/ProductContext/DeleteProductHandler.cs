using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class DeleteProductHandler : ICommandHandler<DeleteProduct>
{
    private readonly ICrudRepository<Product> _repository;

    public DeleteProductHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteProduct command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(command.Id, cancellationToken);
        product.Delete();
        await _repository.SaveChangesAsync(cancellationToken);
    }
}