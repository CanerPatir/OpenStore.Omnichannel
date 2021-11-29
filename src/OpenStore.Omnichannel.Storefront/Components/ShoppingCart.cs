using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Storefront.Models.Checkout;
using OpenStore.Omnichannel.Storefront.Services;

namespace OpenStore.Omnichannel.Storefront.Components;

public class ShoppingCart : ViewComponent
{
    private readonly CheckoutBffService _checkoutBffService;

    public ShoppingCart(CheckoutBffService checkoutBffService)
    {
        _checkoutBffService = checkoutBffService;
    }

    public async Task<IViewComponentResult> InvokeAsync(ShoppingCartViewModel model, CancellationToken cancellationToken = default)
    {
        model ??= await _checkoutBffService.GetShoppingCartViewModel(cancellationToken);

        return View(model);
    }
}