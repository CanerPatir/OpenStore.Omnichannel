using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.CheckoutContext;
using OpenStore.Omnichannel.Shared.Query.Storefront;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.ReadModel.Sql.Storefront;

public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, ShoppingCartResult>
{
    private readonly ICrudRepository<ShoppingCart> _repository;

    public GetShoppingCartQueryHandler(ICrudRepository<ShoppingCart> repository)
    {
        _repository = repository;
    }

    public async Task<ShoppingCartResult> Handle(GetShoppingCartQuery query, CancellationToken cancellationToken)
    {
        var shoppingCart = await _repository.GetAsync(query.CartId, cancellationToken);
        return new ShoppingCartResult(
            shoppingCart.Id,
            shoppingCart.IsAuthenticated,
            shoppingCart.Items.Select(x => new ShoppingCartItemDto(x.Id, x.VariantId, x.Quantity)).ToList()
        );
    }
}