using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UpdateProductVariantHandler : ICommandHandler<UpdateProductVariant>
{
    private readonly ICrudRepository<Product> _repository;

    public UpdateProductVariantHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateProductVariant command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(command.ProductId, cancellationToken);
        product.UpdateVariant(command);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}