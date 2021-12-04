using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Domain;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UpdateProductCollectionHandler : IRequestHandler<UpdateProductCollection>
{
    private readonly ICrudRepository<ProductCollection> _repository;

    public UpdateProductCollectionHandler(ICrudRepository<ProductCollection> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateProductCollection command, CancellationToken cancellationToken)
    {
        var productCollection = await _repository.GetAsync(command.ProductCollectionId, cancellationToken);
        if (productCollection.Handle != command.Model.Handle)
        {
            if (await _repository.Query.AnyAsync(x => x.Handle == command.Model.Handle && x.Id != command.ProductCollectionId, cancellationToken))
            {
                throw new DomainException(Msg.Domain.ProductCollection.ProductCollectionHandleAlreadyExists);
            }
        }
        productCollection.Update(command);

        await _repository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}