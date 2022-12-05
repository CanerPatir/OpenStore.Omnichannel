using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.CheckoutContext;
using OpenStore.Omnichannel.Shared.Command.CheckoutContext;

namespace OpenStore.Omnichannel.Application.Command.CheckoutContext;

public class BindCartToUserHandler : CommandHandler<BindCartToUser>
{
    private readonly ICrudRepository<ShoppingCart> _repository;

    public BindCartToUserHandler(ICrudRepository<ShoppingCart> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(BindCartToUser command, CancellationToken cancellationToken)
    {
        var shoppingCart = await _repository.GetAsync(command.CartId, cancellationToken);
        if (shoppingCart is null) throw new ApplicationException(Msg.Application.CartNotExists);
        shoppingCart.BindToUser(command);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}