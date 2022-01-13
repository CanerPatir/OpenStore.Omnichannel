namespace OpenStore.Omnichannel.Shared.Dto.Identity;

public record ApplicationUserAddressDto(
    Guid Id
    , string Firstname
    , string Surname
    , string PhoneNumber
    , string City
    , string Town
    , string District
    , string AddressDescription
    , string PostCode
    , string AddressName);
 