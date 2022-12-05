using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Domain;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UpdateProductCollectionHandler : CommandHandler<UpdateProductCollection>
{
    private readonly ICrudRepository<ProductCollection> _repository;

    public UpdateProductCollectionHandler(ICrudRepository<ProductCollection> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(UpdateProductCollection command, CancellationToken cancellationToken)
    {
        var productCollection = await _repository.GetAsync(command.ProductCollectionId, cancellationToken);
        if (productCollection.Handle != command.Model.Handle)
            if (await _repository.Query.AnyAsync(x => x.Handle == command.Model.Handle && x.Id != command.ProductCollectionId, cancellationToken))
                throw new DomainException(Msg.Domain.ProductCollection.ProductCollectionHandleAlreadyExists);
        productCollection.Update(command);

        await _repository.SaveChangesAsync(cancellationToken);
    }
}