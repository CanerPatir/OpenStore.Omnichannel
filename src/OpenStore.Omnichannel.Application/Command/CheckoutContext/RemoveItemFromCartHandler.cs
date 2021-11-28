using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.CheckoutContext;

namespace OpenStore.Omnichannel.Application.Command.CheckoutContext;

public class RemoveItemFromCartHandler : AsyncRequestHandler<RemoveItemFromCart>
{
    private readonly ICrudRepository<ShoppingCart> _repository;

    public RemoveItemFromCartHandler(ICrudRepository<ShoppingCart> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(RemoveItemFromCart command, CancellationToken cancellationToken)
    {
        var shoppingCart = await _repository.GetAsync(command.CartId, cancellationToken);
        if (shoppingCart is null)
        {
            throw new ApplicationException(Msg.Application.CartNotExists);
        }
        if (shoppingCart.RemoveItem(command) == 0)
        {
            return;
        }
        await _repository.SaveChangesAsync(cancellationToken);
    }
}