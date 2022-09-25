namespace OpenStore.Omnichannel.Shared.Command.OrderContext;

public record CreatePreorderCommand(IEnumerable<CreatePreorderCommand.OrderLineItem> LineItems)
{
    public record OrderLineItem(Guid ProductId
        , Guid VariantId
        , string Barcode
        , string Sku
        , string Title
        , string PhotoUrl
        , string Brand
        , decimal Price
        , CurrencyCode CurrencyCode
        , int Quantity
        , bool Taxable
        , decimal? Tax
        , CurrencyCode? TaxCurrencyCode
        , bool RequiresShipping);
};

public record FulfillOrder(Guid Id, string TrackingNumber, string CarrierIdentifier, IDictionary<Guid, int> LineItemQuantities) : ICommand<Guid>;