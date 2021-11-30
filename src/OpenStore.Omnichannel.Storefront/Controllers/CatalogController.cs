using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Storefront.Models.Catalog;
using OpenStore.Omnichannel.Storefront.Services;

namespace OpenStore.Omnichannel.Storefront.Controllers;

public class CatalogController : Controller
{
    private const string SearchRouteNameEn = "Search";
    private const string SearchRouteNameTr = "arama";
    private const string AllRouteName = "All";
    private const string CollectionRouteName = "Collection";
    private const string ProductDetailRouteName = "ProductDetail";

    private readonly ProductBffService _productBffService;

    public CatalogController(ProductBffService productBffService)
    {
        _productBffService = productBffService;
    }

    [HttpGet("~/search", Name = SearchRouteNameEn)]
    [HttpGet("~/arama", Name = SearchRouteNameTr)]
    public IActionResult Search([FromQuery] string term) => View(new SearchViewModel(term));

    [HttpGet("~/collection", Name = CollectionRouteName)]
    public IActionResult Collection([FromQuery] string name, [FromQuery] Guid id) => View();

    [HttpGet("~/all", Name = AllRouteName)]
    public IActionResult AllProducts() => View();

    [HttpGet("~/product/{handle}", Name = ProductDetailRouteName)]
    public async Task<IActionResult> ProductDetail(string handle) => View(await _productBffService.GetProductDetailViewModel(handle, HttpContext.RequestAborted));

    #region AjaxRequests

    [HttpGet("~/search/page")]
    public async Task<IActionResult> SearchProductsPage([FromQuery] string term, [FromQuery] int page) => Ok(new
    {
        data = await _productBffService.GetSearchProductsPage(term, page, HttpContext.RequestAborted)
    });

    [HttpGet("~/collection/page")]
    public async Task<IActionResult> CollectionProductsPage([FromQuery] Guid id, [FromQuery] int page) => Ok(new
    {
        data = await _productBffService.GetCollectionProductsPage(id, page, HttpContext.RequestAborted)
    });

    [HttpGet("~/all/page")]
    public async Task<IActionResult> AllProductsPage([FromQuery] int page) => Ok(new
    {
        data = await _productBffService.GetAllProductsPage(page, HttpContext.RequestAborted)
    });

    #endregion
}