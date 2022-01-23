using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UpdateProductVariantSkusHandler : CommandHandler<UpdateProductVariantSkus>
{
    private readonly ICrudRepository<Product> _repository;

    public UpdateProductVariantSkusHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(UpdateProductVariantSkus command, CancellationToken cancellationToken)
    {
        var product = await _repository.Query.Include(x => x.Variants).SingleOrDefaultAsync(x => x.Id == command.ProductId, cancellationToken);
        product.UpdateVariantSkus(command);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}