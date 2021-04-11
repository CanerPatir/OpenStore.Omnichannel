using System.ComponentModel.DataAnnotations;

namespace OpenStore.Omnichannel.Identity.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = Msg.Validation.Required)]
        public string Name { get; set; }

        [Required(ErrorMessage = Msg.Validation.Required)]
        public string Surname { get; set; }

        public string Gender { get; set; }

        public bool AcceptPrivacyPolicy { get; set; } = true;

        [Required(ErrorMessage = Msg.Validation.Required)]
        [EmailAddress(ErrorMessage = Msg.Validation.InvalidEmail)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = Msg.Validation.Required)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = Msg.Validation.PasswordMismatch)]
        public string ConfirmPassword { get; set; }
    }
}