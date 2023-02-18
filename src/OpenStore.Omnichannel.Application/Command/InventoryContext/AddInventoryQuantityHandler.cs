using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.InventoryContext;
using OpenStore.Omnichannel.Shared.Command.InventoryContext;

namespace OpenStore.Omnichannel.Application.Command.InventoryContext;

public class AddInventoryQuantityHandler : ICommandHandler<AddInventoryQuantity>
{
    private readonly ICrudRepository<Inventory> _repository;

    public AddInventoryQuantityHandler(ICrudRepository<Inventory> repository)
    {
        _repository = repository;
    }

    public async Task Handle(AddInventoryQuantity command, CancellationToken cancellationToken)
    {
        var inventory = await _repository.GetAsync(command.Id, cancellationToken);

        inventory.AddQuantity(command);

        await _repository.SaveChangesAsync(cancellationToken);
    }
}