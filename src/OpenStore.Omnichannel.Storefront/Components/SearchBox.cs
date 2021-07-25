using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Omnichannel.Storefront.Models.Components;

namespace OpenStore.Omnichannel.Storefront.Components
{
    public class SearchBox : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {
            var term = Convert.ToString(ViewContext.RouteData.Values[nameof(SearchBoxViewModel.Term)] ?? "");
            return Task.FromResult((IViewComponentResult)View(new SearchBoxViewModel(term)));
        }
    }
}