using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UpdateProductVariantHandler : CommandHandler<UpdateProductVariant>
{
    private readonly ICrudRepository<Product> _repository;

    public UpdateProductVariantHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(UpdateProductVariant command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(command.ProductId, cancellationToken);
        product.UpdateVariant(command);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}