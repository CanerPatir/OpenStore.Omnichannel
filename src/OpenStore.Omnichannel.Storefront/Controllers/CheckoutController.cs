using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Shared.Dto.Identity;
using OpenStore.Omnichannel.Storefront.Models.Checkout;
using OpenStore.Omnichannel.Storefront.Services;

namespace OpenStore.Omnichannel.Storefront.Controllers;

[Route("[controller]")]
public class CheckoutController : Controller
{
    private const string ShoppingCartRouteName = "Cart";

    private readonly CheckoutBffService _checkoutBffService;
    private readonly UserBffService _userBffService;

    public CheckoutController(CheckoutBffService checkoutBffService, UserBffService userBffService)
    {
        _checkoutBffService = checkoutBffService;
        _userBffService = userBffService;
    }

    #region CheckoutProgress

    [Authorize]
    [HttpGet("~/checkout", Name = nameof(Checkout))]
    public async Task<IActionResult> Checkout(bool? addAddressFailed)
    {
        ViewBag.addAddressFailed = addAddressFailed ?? false;
        var myAddresses = await _userBffService.GetMyAddresses(HttpContext.RequestAborted);
        return View(myAddresses.Select(x => new AddressViewModel
        {
            Id = x.Id,
            City = x.City,
            District = x.District,
            Firstname = x.Firstname,
            Surname = x.Surname,
            Town = x.Town,
            AddressDescription = x.AddressDescription,
            AddressName = x.AddressName,
            PhoneNumber = x.PhoneNumber,
            PostCode = x.PostCode
        }));
    }

    [Authorize]
    [HttpGet("~/payment", Name = nameof(Payment))]
    public IActionResult Payment() => View();

    [Authorize]
    [HttpGet("~/confirm", Name = nameof(Confirm))]
    public IActionResult Confirm() => View();

    [Authorize]
    [HttpPost("~/continue", Name = nameof(Continue))]
    public async Task<IActionResult> Continue()
    {
        var page = Request.Form["page"].ToString();

        if (page == nameof(Checkout))
        {
            return RedirectToRoute(nameof(Payment));
        }

        if (page == nameof(Payment))
        {
            return RedirectToRoute(nameof(Confirm));
        }

        return BadRequest();
    }

    [Authorize]
    [HttpPost("~/add-address", Name = nameof(AddAddress))]
    public async Task<IActionResult> AddAddress([FromForm] AddressViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToRoute(nameof(Checkout), new { addAddressFailed = true });
        }

        await _userBffService.AddAddressToMe(new ApplicationUserAddressDto(model.Id
            , model.Firstname
            , model.Surname
            , model.PhoneNumber
            , model.City
            , model.Town
            , model.District
            , model.AddressDescription
            , model.PostCode
            , model.AddressName), HttpContext.RequestAborted);
        return RedirectToRoute(nameof(Checkout));
    } 
    
    [Authorize]
    [HttpPost("~/update-address", Name = nameof(UpdateAddress))]
    public async Task<IActionResult> UpdateAddress([FromForm] AddressViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToRoute(nameof(Checkout), new { addAddressFailed = true });
        }

        await _userBffService.AddAddressToMe(new ApplicationUserAddressDto(model.Id
            , model.Firstname
            , model.Surname
            , model.PhoneNumber
            , model.City
            , model.Town
            , model.District
            , model.AddressDescription
            , model.PostCode
            , model.AddressName), HttpContext.RequestAborted);
        return RedirectToRoute(nameof(Checkout));
    }

    #endregion

    #region ShoppingCart

    [HttpGet("~/cart", Name = ShoppingCartRouteName)]
    public IActionResult ShoppingCart() => View();

    [HttpPost]
    public async Task<IActionResult> UpdateShoppingCartItemQuantity([FromForm] Guid cartId, [FromForm] Guid cartItemId, [FromForm] int quantity)
    {
        await _checkoutBffService.ChangeItemQuantityOfCart(cartItemId, quantity, HttpContext.RequestAborted);
        return RedirectToAction(nameof(ShoppingCart));
    }

    #region AjaxRequests

    [HttpPost("shoppingCart/items")]
    public async Task<Guid> AddItemToCart([FromQuery] Guid variantId, [FromQuery] int quantity) =>
        await _checkoutBffService.AddItemToCart(variantId, quantity, HttpContext.RequestAborted);

    [HttpDelete("shoppingCart/items/{itemId:guid}")]
    public async Task RemoveItemFromCart(Guid itemId) => await _checkoutBffService.RemoveItemFromCart(itemId, HttpContext.RequestAborted);

    [HttpPost("shoppingCart/items/{itemId:guid}")]
    public async Task RemoveItemFromCart(Guid itemId, [FromQuery] int quantity) =>
        await _checkoutBffService.ChangeItemQuantityOfCart(itemId, quantity, HttpContext.RequestAborted);

    #endregion

    #endregion
}