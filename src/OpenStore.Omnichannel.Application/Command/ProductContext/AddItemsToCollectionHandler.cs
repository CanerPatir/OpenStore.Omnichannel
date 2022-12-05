using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class AddItemsToCollectionHandler : CommandHandler<AddItemsToCollection>
{
    private readonly ICrudRepository<ProductCollection> _repository;

    public AddItemsToCollectionHandler(ICrudRepository<ProductCollection> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(AddItemsToCollection command, CancellationToken cancellationToken)
    {
        var (productCollectionId, productIds) = command;
        var productCollection = await _repository.Query
            .Include(x => x.ProductItems)
            .SingleOrDefaultAsync(x => x.Id == productCollectionId, cancellationToken);

        foreach (var productId in productIds) productCollection.AddProduct(productId);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}