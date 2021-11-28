using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Domain.CheckoutContext;
using OpenStore.Omnichannel.Infrastructure.Authentication;

namespace OpenStore.Omnichannel.Api.Storefront;

[Route("api-sf/[controller]")]
public class ShoppingCartController : BaseApiController
{
    private readonly IMediator _mediator;

    public ShoppingCartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<Guid> CreateOrGetShoppingCart() => _mediator.Send(new CreateShoppingCart(User.Identity?.IsAuthenticated == true ? User.GetId() : null), CancellationToken);

    [HttpPost("{id:guid}/items")]
    public Task<Guid> AddItemToCart(Guid id, [FromQuery] Guid variantId, [FromQuery] int quantity) => _mediator.Send(new AddItemToCart(id, variantId, quantity), CancellationToken);

    [HttpDelete("{id:guid}/items/{itemId:guid}")]
    public Task RemoveItemFromCart(Guid id, Guid itemId) => _mediator.Send(new RemoveItemFromCart(id, itemId), CancellationToken);

    [HttpPost("{id:guid}/items/{itemId:guid}")]
    public Task ChangeItemQuantityOfCart(Guid id, Guid itemId, [FromQuery] int quantity) => _mediator.Send(new ChangeItemQuantityOfCart(id, itemId, quantity), CancellationToken);
    
    [HttpPost("{id:guid}/bind-to-user/{userId:guid}")]
    public Task BindCartToUser(Guid id, Guid userId) => _mediator.Send(new BindCartToUser(id, userId), CancellationToken);
}