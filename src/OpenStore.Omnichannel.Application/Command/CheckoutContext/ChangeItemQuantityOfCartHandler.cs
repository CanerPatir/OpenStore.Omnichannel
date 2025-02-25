using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.CheckoutContext;
using OpenStore.Omnichannel.Shared.Command.CheckoutContext;

namespace OpenStore.Omnichannel.Application.Command.CheckoutContext;

public class ChangeItemQuantityOfCartHandler : ICommandHandler<ChangeItemQuantityOfCart>
{
    private readonly ICrudRepository<ShoppingCart> _repository;

    public ChangeItemQuantityOfCartHandler(ICrudRepository<ShoppingCart> repository)
    {
        _repository = repository;
    }

    public async Task Handle(ChangeItemQuantityOfCart command, CancellationToken cancellationToken)
    {
        var shoppingCart = await _repository.GetAsync(command.CartId, cancellationToken);
        if (shoppingCart is null) throw new ApplicationException(Msg.Application.CartNotExists);
        shoppingCart.ChangeItemQuantity(command);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}