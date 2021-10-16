using System;
using System.ComponentModel.DataAnnotations;

namespace OpenStore.Omnichannel.Shared.Dto;

public class ApplicationUserDto
{
    public Guid Id { get; set; }

    [EmailAddress(ErrorMessage = Msg.Validation.InvalidEmail)]
    [Required(ErrorMessage = Msg.Validation.Required)]
    public string Email { get; set; }

    [Required(ErrorMessage = Msg.Validation.Required)]
    public string Name { get; set; }

    [Required(ErrorMessage = Msg.Validation.Required)]
    public string Surname { get; set; }

    public string PhotoPath { get; set; }
    public string PhoneNumber { get; set; }

    public string FullName => $"{Name} {Surname}";

    public bool EmailConfirmed { get; set; }
}