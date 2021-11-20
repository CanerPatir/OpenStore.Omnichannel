using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.CheckoutContext;

namespace OpenStore.Omnichannel.Application.Command.CheckoutContext;

public class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCart, Guid>
{
    private readonly ICrudRepository<ShoppingCart> _repository;

    public CreateShoppingCartHandler(ICrudRepository<ShoppingCart> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateShoppingCart request, CancellationToken cancellationToken)
    {
        // todo: Get existing first
        throw new NotImplementedException();
    }
}
