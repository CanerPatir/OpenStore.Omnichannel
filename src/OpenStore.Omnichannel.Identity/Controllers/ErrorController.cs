using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Identity.ViewModels.Shared;

namespace OpenStore.Omnichannel.Identity.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : Controller
{
    [HttpGet, HttpPost, Route("~/error")]
    public IActionResult Index(int? statusCode)
    {
        // If the error was not caused by an invalid
        // OIDC request, display a generic error page.
        var response = HttpContext.GetOpenIddictServerResponse();
        if (response == null)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context?.Error == null)
            {
                return View(new ErrorViewModel
                {
                    StatusCode = statusCode
                });
            }

            return View(new ErrorViewModel
            {
                Error = context.Error.Message,
                // ErrorDescription = context.Error.ToString(),
                StatusCode = statusCode
            });
        }

        return View(new ErrorViewModel
        {
            Error = response.Error,
            ErrorDescription = response.ErrorDescription,
            StatusCode = statusCode
        });
    }
}