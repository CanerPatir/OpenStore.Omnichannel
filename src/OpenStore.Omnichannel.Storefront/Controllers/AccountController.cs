using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OpenStore.Omnichannel.Storefront.Controllers;

[Authorize]
public class AccountController : Controller
{
    // GET
    public IActionResult Index() => View();

    [HttpPost]
    public new IActionResult SignOut() => SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
}