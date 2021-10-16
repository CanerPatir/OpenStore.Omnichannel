using System.ComponentModel.DataAnnotations;

namespace OpenStore.Omnichannel.Identity.ViewModels.Manage;

public class ChangePasswordViewModel
{
    [Required(ErrorMessage = Msg.Validation.Required)]
    [DataType(DataType.Password)]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = Msg.Validation.Required)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = Msg.Validation.PasswordMismatch)]
    public string ConfirmPassword { get; set; }
}