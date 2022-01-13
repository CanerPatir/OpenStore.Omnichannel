using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Storefront.Services;

namespace OpenStore.Omnichannel.Storefront.Controllers;

[Route("[controller]")]
public class CheckoutController : Controller
{
    private const string ShoppingCartRouteName = "Cart";
    
    private readonly CheckoutBffService _checkoutBffService;
    private readonly UserBffService _userBffService;

    public CheckoutController(CheckoutBffService checkoutBffService, UserBffService userBffService)
    {
        _checkoutBffService = checkoutBffService;
        _userBffService = userBffService;
    }

    [HttpGet("~/cart", Name = ShoppingCartRouteName)]
    public IActionResult ShoppingCart() => View();

    [HttpPost]
    public async Task<IActionResult> UpdateShoppingCartItemQuantity([FromForm] Guid cartId, [FromForm] Guid cartItemId, [FromForm] int quantity)
    {
        await _checkoutBffService.ChangeItemQuantityOfCart(cartItemId, quantity, HttpContext.RequestAborted);
        return RedirectToAction(nameof(ShoppingCart));
    }
    
    [Authorize]
    [HttpGet("~/checkout", Name = nameof(Checkout))]
    public async Task<IActionResult> Checkout()
    {
        var myAddresses = await _userBffService.GetMyAddresses(HttpContext.RequestAborted);
        return View();
    }
    
    [Authorize]
    [HttpGet("~/payment", Name = nameof(Payment))]
    public IActionResult Payment() => View();
    
    [Authorize]
    [HttpGet("~/confirm", Name = nameof(Confirm))]
    public IActionResult Confirm() => View();

    [Authorize]
    [HttpPost("~/continue", Name = nameof(Continue))]
    public async Task<IActionResult> Continue()
    {
        var page = Request.Form["page"].ToString();

        if (page == nameof(Checkout))
        {
            return RedirectToRoute(nameof(Payment));
   
        }
        
        if (page == nameof(Payment))
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