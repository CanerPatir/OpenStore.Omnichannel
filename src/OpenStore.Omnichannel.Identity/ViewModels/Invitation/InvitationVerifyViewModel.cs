using System.ComponentModel.DataAnnotations;

namespace OpenStore.Omnichannel.Identity.ViewModels.Invitation;

public class InvitationVerifyViewModel
{
    [Required(ErrorMessage = Msg.Validation.Required)]
    public string Code { get; set; }

    [Required(ErrorMessage = Msg.Validation.Required)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string NewPassword { get; set; }

    [Display(Name = "Confirm new password")]
    [Compare("NewPassword", ErrorMessage = Msg.Validation.PasswordMismatch)]
    public string ConfirmPassword { get; set; }
}