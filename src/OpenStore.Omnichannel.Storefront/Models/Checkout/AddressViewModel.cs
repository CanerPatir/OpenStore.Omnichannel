using System.ComponentModel.DataAnnotations;

namespace OpenStore.Omnichannel.Storefront.Models.Checkout;

public class AddressViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "General.Validation.Required")]
    public string Firstname { get; set; }

    [Required(ErrorMessage = "General.Validation.Required")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "General.Validation.Required")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "General.Validation.Required")]
    public string City { get; set; }

    [Required(ErrorMessage = "General.Validation.Required")]
    public string Town { get; set; }

    [Required(ErrorMessage = "General.Validation.Required")]
    public string District { get; set; }

    [Required(ErrorMessage = "General.Validation.Required")]
    public string AddressDescription { get; set; }

    public string PostCode { get; set; }

    [Required(ErrorMessage = "General.Validation.Required")]
    public string AddressName { get; set; }
}