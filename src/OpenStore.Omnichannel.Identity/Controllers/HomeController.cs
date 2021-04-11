using Microsoft.AspNetCore.Mvc;

namespace OpenStore.Omnichannel.Identity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Hold()
        {
            return View();
        }
    }
}
