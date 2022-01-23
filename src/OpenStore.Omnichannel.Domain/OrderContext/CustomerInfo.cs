namespace OpenStore.Omnichannel.Domain.OrderContext;

public record CustomerInfo(Guid UserId,
    string Email,
    string Phone,
    string Currency,
    string Surname,
    string Firstname,
    string CustomerLocale);