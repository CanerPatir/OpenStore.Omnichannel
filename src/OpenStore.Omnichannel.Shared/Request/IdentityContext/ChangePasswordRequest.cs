using System.ComponentModel.DataAnnotations;

namespace OpenStore.Omnichannel.Shared.Request.IdentityContext;

public class ChangePasswordRequest
{
    [Required(ErrorMessage = Msg.Validation.Required)]
    [DataType(DataType.Password)]
    [Display(Name = "Current password")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = Msg.Validation.Required)]
    [StringLength(100, ErrorMessage = "Parola en az {2} karakter olmalı", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = Msg.Validation.PasswordMismatch)]
    public string ConfirmPassword { get; set; }
}