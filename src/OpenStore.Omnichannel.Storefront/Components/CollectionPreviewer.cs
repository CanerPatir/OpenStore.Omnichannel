using Microsoft.AspNetCore.Mvc;

namespace OpenStore.Omnichannel.Storefront.Components;

public class CollectionPreviewer : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}