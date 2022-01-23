using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Domain.CheckoutContext;
using OpenStore.Omnichannel.Shared.Command.CheckoutContext;
using OpenStore.Omnichannel.Shared.Query.Storefront;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.Api.Storefront;

[Route("api-sf/[controller]")]
public class CheckoutController : BaseApiController
{
    private readonly IMediator _mediator;

    public CheckoutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Shopping Cart

    [HttpPost("shopping-cart/")]
    public Task<Guid> CreateOrGetShoppingCart([FromQuery] Guid? userId) => _mediator.Send(new CreateShoppingCart(userId), CancellationToken);

    [HttpPost("shopping-cart/{id:guid}/items")]
    public Task<Guid> AddItemToCart(Guid id, [FromQuery] Guid variantId, [FromQuery] int quantity) => _mediator.Send(new AddItemToCart(id, variantId, quantity), CancellationToken);

    [HttpGet("shopping-cart/{id:guid}")]
    public Task<ShoppingCartQueryResult> GetCart(Guid id) => _mediator.Send(new GetShoppingCartQuery(id), CancellationToken);

    [HttpDelete("shopping-cart/{id:guid}/items/{itemId:guid}")]
    public Task RemoveItemFromCart(Guid id, Guid itemId) => _mediator.Send(new RemoveItemFromCart(id, itemId), CancellationToken);

    [HttpPost("shopping-cart/{id:guid}/items/{itemId:guid}")]
    public Task ChangeItemQuantityOfCart(Guid id, Guid itemId, [FromQuery] int quantity) => _mediator.Send(new ChangeItemQuantityOfCart(id, itemId, quantity), CancellationToken);

    [HttpPost("shopping-cart/{id:guid}/bind-to-user/{userId:guid}")]
    public Task BindCartToUser(Guid id, Guid userId) => _mediator.Send(new BindCartToUser(id, userId), CancellationToken);

    #endregion

    #region Checkout

    [HttpPost("create-preorder")]
    public Task CreatePreorder() => _mediator.Send(null, CancellationToken);

    #endregion
}