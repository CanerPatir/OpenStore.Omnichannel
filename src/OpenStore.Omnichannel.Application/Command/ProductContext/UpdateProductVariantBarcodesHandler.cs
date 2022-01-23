using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UpdateProductVariantBarcodesHandler : CommandHandler<UpdateProductVariantBarcodes>
{
    private readonly ICrudRepository<Product> _repository;

    public UpdateProductVariantBarcodesHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(UpdateProductVariantBarcodes command, CancellationToken cancellationToken)
    {
        var product = await _repository.Query.Include(x => x.Variants).SingleOrDefaultAsync(x => x.Id == command.ProductId, cancellationToken);
        product.UpdateVariantBarcodes(command);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}