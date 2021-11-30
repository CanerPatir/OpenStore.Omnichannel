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

    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken = default)
    {
        await _checkoutBffService.CreateShoppingCartIfNotExists(cancellationToken);
        var viewModel = await _checkoutBffService.GetFlyoutShoppingCartViewModel(cancellationToken);
        return View(viewModel);
    }
}
