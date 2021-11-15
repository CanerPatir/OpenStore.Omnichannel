using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Storefront.Services;

namespace OpenStore.Omnichannel.Storefront.Controllers;

public class CatalogController : Controller
{
    private const string SearchRouteName = "Search";
    private const string AllRouteName = "All";
    private const string CollectionRouteName = "Collection";

    private readonly ProductBffService _productBffService;
    private readonly CollectionProductsViewModelFactory _collectionProductsViewModelFactory;

    public CatalogController(ProductBffService productBffService, CollectionProductsViewModelFactory collectionProductsViewModelFactory)
    {
        _productBffService = productBffService;
        _collectionProductsViewModelFactory = collectionProductsViewModelFactory;
    }

    [HttpGet("~/search", Name = SearchRouteName)]
    public IActionResult Search([FromQuery] string term) => View();

    [HttpGet("~/all", Name = AllRouteName)]
    public IActionResult AllProducts() => View();
    
    [HttpGet("~/all-products/page")]
    public async Task<IActionResult> AllProductsPage([FromQuery] int page) => Ok(new
    {
        data = await _productBffService.GetProductsPage(page)
    });

    [HttpGet("~/collection/{name}", Name = CollectionRouteName)]
    public async Task<IActionResult> Collection(string name) => View(await _collectionProductsViewModelFactory.Produce());
}