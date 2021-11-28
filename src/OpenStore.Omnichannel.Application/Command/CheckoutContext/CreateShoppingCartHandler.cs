using MediatR;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Guid> Handle(CreateShoppingCart command, CancellationToken cancellationToken)
    {
        if (command.UserId.HasValue)
        {
            var existingCart = await _repository
                .Query
                .SingleOrDefaultAsync(x => x.UserId == command.UserId.Value, cancellationToken);

            if (existingCart is not null)
            {
                return existingCart.Id;
            }
        }

        var newShoppingCart = ShoppingCart.Create(command);
        await _repository.InsertAsync(newShoppingCart, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return newShoppingCart.Id;
    }
}