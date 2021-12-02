using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class AddProductToCollectionHandler : IRequestHandler<AddProductToCollection>
{
    private readonly ICrudRepository<ProductCollection> _repository;

    public AddProductToCollectionHandler(ICrudRepository<ProductCollection> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(AddProductToCollection command, CancellationToken cancellationToken)
    {
        var productCollection = await _repository.GetAsync(command.ProductCollectionId, cancellationToken);
        
        productCollection.AddProduct(command);
        await _repository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}