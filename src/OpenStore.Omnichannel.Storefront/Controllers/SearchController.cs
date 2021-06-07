using Microsoft.AspNetCore.Mvc;

namespace OpenStore.Omnichannel.Storefront.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index(string search)
        {
            return View();
        }
    }
}