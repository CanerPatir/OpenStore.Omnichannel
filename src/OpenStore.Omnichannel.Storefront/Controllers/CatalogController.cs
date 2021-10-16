using Microsoft.AspNetCore.Mvc;

namespace OpenStore.Omnichannel.Storefront.Controllers;

public class CatalogController : Controller
{
    private const string SearchRouteName = "Search";

    [HttpGet("~/search", Name = SearchRouteName)]
    public IActionResult Search([FromQuery] string term) => View();
}