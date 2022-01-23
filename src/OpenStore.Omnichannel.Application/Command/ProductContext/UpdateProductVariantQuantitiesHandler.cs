using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UpdateProductVariantQuantitiesHandler : CommandHandler<UpdateProductVariantQuantities>
{
    private readonly ICrudRepository<Product> _repository;

    public UpdateProductVariantQuantitiesHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(UpdateProductVariantQuantities command, CancellationToken cancellationToken)
    {
        var product = await _repository.Query.Include(x => x.Variants).SingleOrDefaultAsync(x => x.Id == command.ProductId, cancellationToken);
        product.UpdateVariantQuantities(command);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}