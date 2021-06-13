using Microsoft.AspNetCore.Mvc;

namespace OpenStore.Omnichannel.Storefront.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Index() => View();
    }
}