using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UpdateProductVariantQuantitiesHandler : ICommandHandler<UpdateProductVariantQuantities>
{
    private readonly ICrudRepository<Product> _repository;

    public UpdateProductVariantQuantitiesHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateProductVariantQuantities command, CancellationToken cancellationToken)
    {
        var product = await _repository.Query.Include(x => x.Variants).SingleOrDefaultAsync(x => x.Id == command.ProductId, cancellationToken);
        product.UpdateVariantQuantities(command);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}