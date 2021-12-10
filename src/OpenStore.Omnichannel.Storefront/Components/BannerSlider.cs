using Microsoft.AspNetCore.Mvc;

namespace OpenStore.Omnichannel.Storefront.Components;

public class BannerSlider : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}