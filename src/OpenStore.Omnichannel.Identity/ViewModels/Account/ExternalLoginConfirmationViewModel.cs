using System.ComponentModel.DataAnnotations;

namespace OpenStore.Omnichannel.Identity.ViewModels.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = Msg.Validation.Required)]
        [EmailAddress(ErrorMessage = Msg.Validation.InvalidEmail)]
        public string Email { get; set; }
    }
}