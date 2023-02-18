using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.InventoryContext;
using OpenStore.Omnichannel.Shared.Command.InventoryContext;

namespace OpenStore.Omnichannel.Application.Command.InventoryContext;

public class SetInventoryQuantityHandler : ICommandHandler<SetInventoryQuantity>
{
    private readonly ICrudRepository<Inventory> _repository;

    public SetInventoryQuantityHandler(ICrudRepository<Inventory> repository)
    {
        _repository = repository;
    }

    public async Task Handle(SetInventoryQuantity command, CancellationToken cancellationToken)
    {
        var inventory = await _repository.GetAsync(command.Id, cancellationToken);

        inventory.SetQuantity(command);

        await _repository.SaveChangesAsync(cancellationToken);
    }
}