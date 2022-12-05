using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.CheckoutContext;
using OpenStore.Omnichannel.Shared.Command.CheckoutContext;

namespace OpenStore.Omnichannel.Application.Command.CheckoutContext;

public class AddItemToCartHandler : ICommandHandler<AddItemToCart, Guid>
{
    private readonly ICrudRepository<ShoppingCart> _repository;

    public AddItemToCartHandler(ICrudRepository<ShoppingCart> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(AddItemToCart command, CancellationToken cancellationToken)
    {
        var shoppingCart = await _repository.GetAsync(command.CartId, cancellationToken);
        if (shoppingCart is null) throw new ApplicationException(Msg.Application.CartNotExists);

        var itemId = shoppingCart.AddItem(command);
        await _repository.SaveChangesAsync(cancellationToken);

        return itemId;
    }
}