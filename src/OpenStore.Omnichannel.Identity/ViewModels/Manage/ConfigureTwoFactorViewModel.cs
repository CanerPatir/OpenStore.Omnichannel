using Microsoft.AspNetCore.Mvc.Rendering;

namespace OpenStore.Omnichannel.Identity.ViewModels.Manage;

public class ConfigureTwoFactorViewModel
{
    public string SelectedProvider { get; set; }

    public ICollection<SelectListItem> Providers { get; set; }
}