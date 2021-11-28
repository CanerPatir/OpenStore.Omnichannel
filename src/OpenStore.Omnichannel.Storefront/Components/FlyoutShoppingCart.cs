using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Storefront.Services;

namespace OpenStore.Omnichannel.Storefront.Components;

public class FlyoutShoppingCart : ViewComponent
{
    private readonly ShoppingCartBffService _shoppingCartBffService;

    public FlyoutShoppingCart(ShoppingCartBffService shoppingCartBffService)
    {
        _shoppingCartBffService = shoppingCartBffService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        await _shoppingCartBffService.CreateShoppingCartIfNotExists();
        return View();
    }
}
