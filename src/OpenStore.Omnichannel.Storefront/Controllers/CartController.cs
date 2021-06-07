using Microsoft.AspNetCore.Mvc;

namespace OpenStore.Omnichannel.Storefront.Controllers
{
    public class CartController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}