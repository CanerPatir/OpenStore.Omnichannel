namespace OpenStore.Omnichannel.Domain.OrderContext;

public class PaymentInfo
{
    public string AvsResultCode { get; set; }
    public string CreditCardBin { get; set; }
    public string CvvResultCode { get; set; }
    public string CreditCardNumber { get; set; }
    public string CreditCardCompany { get; set; }
    
}