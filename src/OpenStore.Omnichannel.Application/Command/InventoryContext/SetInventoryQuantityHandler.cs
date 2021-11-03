using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.InventoryContext;

namespace OpenStore.Omnichannel.Application.Command.InventoryContext;

public class SetInventoryQuantityHandler : AsyncRequestHandler<SetInventoryQuantity>
{
    private readonly ICrudRepository<Inventory> _repository;

    public SetInventoryQuantityHandler(ICrudRepository<Inventory> repository)
    {
        _repository = repository;
    }
    
    protected override async Task Handle(SetInventoryQuantity command, CancellationToken cancellationToken)
    {
        var inventory = await _repository.GetAsync(command.Id, cancellationToken);

        inventory.SetQuantity(command);

        await _repository.SaveChangesAsync(cancellationToken);
    }
}