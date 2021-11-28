using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Storefront.Services;

namespace OpenStore.Omnichannel.Storefront.Controllers;

public class ShoppingCartController : Controller
{    
    public const string ShoppingCartRouteName = "Cart";

    private readonly ShoppingCartBffService _shoppingCartBffService;

    public ShoppingCartController(ShoppingCartBffService shoppingCartBffService)
    {
        _shoppingCartBffService = shoppingCartBffService;
    }
    
    [HttpGet("~/cart", Name = ShoppingCartRouteName)]
    public async Task<IActionResult> Index() => View(await _shoppingCartBffService.GetShoppingCartViewModel(HttpContext.RequestAborted));

    #region AjaxRequests

    
    #endregion
}