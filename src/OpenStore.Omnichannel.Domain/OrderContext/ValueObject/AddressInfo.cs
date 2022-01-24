namespace OpenStore.Omnichannel.Domain.OrderContext;

public record AddressInfo(
    Guid ApplicationUserAddressId,
    string Firstname,
    string Surname,
    string PhoneNumber,
    string City,
    string Town,
    string District,
    string AddressDescription,
    string PostCode,
    string AddressName
);