using System.ComponentModel.DataAnnotations;

namespace OpenStore.Omnichannel.Identity.ViewModels.Account;

public class VerifyCodeViewModel
{
    [Required(ErrorMessage = Msg.Validation.Required)]
    public string Provider { get; set; }

    [Required(ErrorMessage = Msg.Validation.Required)]
    public string Code { get; set; }

    public string ReturnUrl { get; set; }

    [Display(Name = "Remember this browser?")]
    public bool RememberBrowser { get; set; }

    [Display(Name = "Remember me?")] public bool RememberMe { get; set; }
}