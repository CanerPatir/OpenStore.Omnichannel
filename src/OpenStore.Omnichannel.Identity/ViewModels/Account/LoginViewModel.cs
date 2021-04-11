using System.ComponentModel.DataAnnotations;

namespace OpenStore.Omnichannel.Identity.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = Msg.Validation.Required)]
        [EmailAddress(ErrorMessage = Msg.Validation.InvalidEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage = Msg.Validation.Required)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")] public bool RememberMe { get; set; }
    }
}