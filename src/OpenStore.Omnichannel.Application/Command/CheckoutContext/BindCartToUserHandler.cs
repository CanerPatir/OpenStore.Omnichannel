using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.CheckoutContext;

namespace OpenStore.Omnichannel.Application.Command.CheckoutContext;

public class BindCartToUserHandler : AsyncRequestHandler<BindCartToUser>
{
    private readonly ICrudRepository<ShoppingCart> _repository;

    public BindCartToUserHandler(ICrudRepository<ShoppingCart> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(BindCartToUser request, CancellationToken cancellationToken)
    {
    }
}