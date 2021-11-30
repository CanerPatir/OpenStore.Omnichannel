using Microsoft.AspNetCore.Mvc;

namespace OpenStore.Omnichannel.Storefront.Components;

public class FlyoutWishlist : ViewComponent
{
  
    public IViewComponentResult Invoke()
    {
        return View();
    }
}