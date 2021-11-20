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

    protected override async Task Handle(RemoveItemFromCart request, CancellationToken cancellationToken)
    {
    }
}