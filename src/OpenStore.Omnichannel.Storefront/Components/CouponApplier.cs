using Microsoft.AspNetCore.Mvc;

namespace OpenStore.Omnichannel.Storefront.Components;

public class CouponApplier : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync()
    {
         return Task.FromResult((IViewComponentResult)View());
    }
}