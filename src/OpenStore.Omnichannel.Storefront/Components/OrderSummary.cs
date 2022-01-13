using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Storefront.Models.Checkout;
using OpenStore.Omnichannel.Storefront.Services;

namespace OpenStore.Omnichannel.Storefront.Components;

public class OrderSummary : ViewComponent
{
    private readonly CheckoutBffService _checkoutBffService;

    public OrderSummary(CheckoutBffService checkoutBffService)
    {
        _checkoutBffService = checkoutBffService;
    }

    public async Task<IViewComponentResult> InvokeAsync(OrderSummaryViewModel model, bool dontShowPurchaseButton, CancellationToken cancellationToken = default)
    {
        model ??= await _checkoutBffService.GetOrderSummary(cancellationToken);
        ViewBag.DontShowPurchaseButton = dontShowPurchaseButton;
        return View(model);
    }
}