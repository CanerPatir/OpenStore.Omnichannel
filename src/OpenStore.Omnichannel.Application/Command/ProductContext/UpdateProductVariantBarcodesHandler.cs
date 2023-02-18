using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UpdateProductVariantBarcodesHandler : ICommandHandler<UpdateProductVariantBarcodes>
{
    private readonly ICrudRepository<Product> _repository;

    public UpdateProductVariantBarcodesHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateProductVariantBarcodes command, CancellationToken cancellationToken)
    {
        var product = await _repository.Query.Include(x => x.Variants).SingleOrDefaultAsync(x => x.Id == command.ProductId, cancellationToken);
        product.UpdateVariantBarcodes(command);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}