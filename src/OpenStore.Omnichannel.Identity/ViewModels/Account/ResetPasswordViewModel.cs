using System.ComponentModel.DataAnnotations;

namespace OpenStore.Omnichannel.Identity.ViewModels.Account;

public class ResetPasswordViewModel
{
    [Required(ErrorMessage = Msg.Validation.Required)]
    [EmailAddress(ErrorMessage = Msg.Validation.InvalidEmail)]
    public string Email { get; set; }

    [Required(ErrorMessage = Msg.Validation.Required)]
    // [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = Msg.Validation.PasswordMismatch)]
    public string ConfirmPassword { get; set; }

    public string Code { get; set; }
}