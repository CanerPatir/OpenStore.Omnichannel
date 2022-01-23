using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UpdateProductVariantPricesHandler : CommandHandler<UpdateProductVariantPrices>
{
    private readonly ICrudRepository<Product> _repository;

    public UpdateProductVariantPricesHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(UpdateProductVariantPrices command, CancellationToken cancellationToken)
    {
        var product = await _repository.Query.Include(x => x.Variants).SingleOrDefaultAsync(x => x.Id == command.ProductId, cancellationToken);
        product.UpdateVariantPrices(command);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}