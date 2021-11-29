using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Storefront.Services;

namespace OpenStore.Omnichannel.Storefront.Controllers;

[Route("[controller]")]
public class CheckoutController : Controller
{
    private const string ShoppingCartRouteName = "Cart";
    private const string PaymentRouteName = "Payment";

    private readonly CheckoutBffService _checkoutBffService;

    public CheckoutController(CheckoutBffService checkoutBffService)
    {
        _checkoutBffService = checkoutBffService;
    }

    [HttpGet("~/cart", Name = ShoppingCartRouteName)]
    public IActionResult ShoppingCart() => View();
    
    [HttpGet("~/payment", Name = PaymentRouteName)]
    public IActionResult Payment() => View();

    #region AjaxRequests

    [HttpPost("shoppingCart/items")]
    public async Task<Guid> AddItemToCart([FromQuery] Guid variantId, [FromQuery] int quantity) =>
        await _checkoutBffService.AddItemToCart(variantId, quantity, HttpContext.RequestAborted);

    [HttpDelete("shoppingCart/items/{itemId:guid}")]
    public async Task RemoveItemFromCart(Guid itemId) => await _checkoutBffService.RemoveItemFromCart(itemId, HttpContext.RequestAborted);

    [HttpPost("shoppingCart/items/{itemId:guid}")]
    public async Task RemoveItemFromCart(Guid itemId, [FromQuery] int quantity) =>
        await _checkoutBffService.ChangeItemQuantityOfCart(itemId, quantity, HttpContext.RequestAborted);

    #endregion
}