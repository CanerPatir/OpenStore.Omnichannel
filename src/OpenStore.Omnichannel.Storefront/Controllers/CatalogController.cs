using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Storefront.Services;

namespace OpenStore.Omnichannel.Storefront.Controllers;

public class CatalogController : Controller
{
    private const string SearchRouteName = "Search";
    private const string AllRouteName = "All";
    private const string CollectionRouteName = "Collection";

    private readonly AllProductsViewModelFactory _allProductsViewModelFactory;
    private readonly CollectionProductsViewModelFactory _collectionProductsViewModelFactory;

    public CatalogController(AllProductsViewModelFactory allProductsViewModelFactory, CollectionProductsViewModelFactory collectionProductsViewModelFactory)
    {
        _allProductsViewModelFactory = allProductsViewModelFactory;
        _collectionProductsViewModelFactory = collectionProductsViewModelFactory;
    }

    [HttpGet("~/search", Name = SearchRouteName)]
    public IActionResult Search([FromQuery] string term) => View();
    
    [HttpGet("~/all", Name = AllRouteName)]
    public async Task<IActionResult> AllProducts() => View(await _allProductsViewModelFactory.Produce());
    
    [HttpGet("~/collection/{name}", Name = CollectionRouteName)]
    public async Task<IActionResult> Collection(string name) => View(await _collectionProductsViewModelFactory.Produce());
}