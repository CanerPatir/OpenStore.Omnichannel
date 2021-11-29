using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Storefront.Services;

namespace OpenStore.Omnichannel.Storefront.Components;

public class FlyoutShoppingCart : ViewComponent
{
    private readonly CheckoutBffService _checkoutBffService;

    public FlyoutShoppingCart(CheckoutBffService checkoutBffService)
    {
        _checkoutBffService = checkoutBffService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        await _checkoutBffService.CreateShoppingCartIfNotExists();
        return View();
    }
}
