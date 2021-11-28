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

    public Task<IViewComponentResult> InvokeAsync()
    {
        _shoppingCartBffService.CreateShoppingCart();
        return Task.FromResult((IViewComponentResult)View());
    }
}
