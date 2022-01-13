using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Storefront.Services;

namespace OpenStore.Omnichannel.Storefront.Controllers;

[Route("[controller]")]
public class CheckoutController : Controller
{
    private const string ShoppingCartRouteName = "Cart";
    
    private readonly CheckoutBffService _checkoutBffService;

    public CheckoutController(CheckoutBffService checkoutBffService)
    {
        _checkoutBffService = checkoutBffService;
    }

    [HttpGet("~/cart", Name = ShoppingCartRouteName)]
    public IActionResult ShoppingCart() => View();

    [HttpPost]
    public async Task<IActionResult> UpdateShoppingCartItemQuantity([FromForm] Guid cartId, [FromForm] Guid cartItemId, [FromForm] int quantity)
    {
        await _checkoutBffService.ChangeItemQuantityOfCart(cartItemId, quantity, HttpContext.RequestAborted);
        return RedirectToAction(nameof(ShoppingCart));
    }
    
    [HttpGet("~/payment", Name = nameof(Payment))]
    public IActionResult Payment() => View();

    [HttpGet("~/checkout", Name = nameof(Checkout))]
    public IActionResult Checkout() => View();
    
    [HttpGet("~/confirm", Name = nameof(Confirm))]
    public IActionResult Confirm() => View();

    [HttpPost("~/continue", Name = nameof(Continue))]
    public async Task<IActionResult> Continue()
    {
        var page = Request.Form["page"].ToString();

        if (page == nameof(Checkout))
        {
            return RedirectToRoute(nameof(Payment));
   
        } else if (page == nameof(Payment))
        {
            return RedirectToRoute(nameof(Confirm));
        }

        return BadRequest();
    }

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