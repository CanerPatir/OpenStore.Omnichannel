namespace OpenStore.Omnichannel.Domain.OrderContext;

public record Payment(
    PaymentType Type
    , string AvsResultCode
    , string CreditCardBin
    , string CvvResultCode
    , string CreditCardNumber
    , string CreditCardCompany);