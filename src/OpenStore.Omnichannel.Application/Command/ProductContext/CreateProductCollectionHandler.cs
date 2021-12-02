using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class CreateProductCollectionHandler : IRequestHandler<CreateProductCollection, Guid>
{
    private readonly ICrudRepository<ProductCollection> _repository;

    public CreateProductCollectionHandler(ICrudRepository<ProductCollection> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateProductCollection command, CancellationToken cancellationToken)
    {
        var productCollection = ProductCollection.Create(command);

        await _repository.InsertAsync(productCollection, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return productCollection.Id;
    }
}