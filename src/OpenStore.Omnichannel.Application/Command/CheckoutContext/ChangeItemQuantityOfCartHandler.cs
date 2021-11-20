using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.CheckoutContext;

namespace OpenStore.Omnichannel.Application.Command.CheckoutContext;

public class ChangeItemQuantityOfCartHandler : AsyncRequestHandler<ChangeItemQuantityOfCart>
{
    private readonly ICrudRepository<ShoppingCart> _repository;

    public ChangeItemQuantityOfCartHandler(ICrudRepository<ShoppingCart> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(ChangeItemQuantityOfCart request, CancellationToken cancellationToken)
    {
    }
}