using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Storefront.Models.Components;

namespace OpenStore.Omnichannel.Storefront.Components
{
    public class Footer : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult((IViewComponentResult)View(new FooterViewModel()));
        }
    }
}