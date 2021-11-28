using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Storefront.Services;

namespace OpenStore.Omnichannel.Storefront.Controllers;

public class ShoppingCartController : Controller
{
    private const string ShoppingCartRouteName = "Cart";

    private readonly ShoppingCartBffService _shoppingCartBffService;

    public ShoppingCartController(ShoppingCartBffService shoppingCartBffService)
    {
        _shoppingCartBffService = shoppingCartBffService;
    }

    [HttpGet("~/cart", Name = ShoppingCartRouteName)]
    public async Task<IActionResult> Index() => View(await _shoppingCartBffService.GetShoppingCartViewModel(HttpContext.RequestAborted));

    #region AjaxRequests

    [HttpPost("items")]
    public async Task<Guid> AddItemToCart([FromQuery] Guid variantId, [FromQuery] int quantity) =>
        await _shoppingCartBffService.AddItemToCart(variantId, quantity, HttpContext.RequestAborted);

    [HttpDelete("items/{itemId:guid}")]
    public async Task RemoveItemFromCart(Guid itemId) => await _shoppingCartBffService.RemoveItemFromCart(itemId, HttpContext.RequestAborted);

    [HttpPost("items/{itemId:guid}")]
    public async Task RemoveItemFromCart(Guid itemId, [FromQuery] int quantity) =>
        await _shoppingCartBffService.ChangeItemQuantityOfCart(itemId, quantity, HttpContext.RequestAborted);

    #endregion
}