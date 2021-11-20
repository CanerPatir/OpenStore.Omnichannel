using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.CheckoutContext;

namespace OpenStore.Omnichannel.Application.Command.CheckoutContext;

public class AddItemToCartHandler : IRequestHandler<AddItemToCart, Guid>
{
    private readonly ICrudRepository<ShoppingCart> _repository;

    public AddItemToCartHandler(ICrudRepository<ShoppingCart> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(AddItemToCart request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}