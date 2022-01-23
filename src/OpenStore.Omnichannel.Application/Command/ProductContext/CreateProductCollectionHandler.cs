using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Domain;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class CreateProductCollectionHandler : ICommandHandler<CreateProductCollection, Guid>
{
    private readonly ICrudRepository<ProductCollection> _repository;

    public CreateProductCollectionHandler(ICrudRepository<ProductCollection> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateProductCollection command, CancellationToken cancellationToken)
    {
        if (await _repository.Query.AnyAsync(x => x.Handle == command.Model.Handle, cancellationToken))
        {
            throw new DomainException(Msg.Domain.ProductCollection.ProductCollectionHandleAlreadyExists);
        }
        
        var productCollection = ProductCollection.Create(command);
        await _repository.InsertAsync(productCollection, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return productCollection.Id;
    }
}