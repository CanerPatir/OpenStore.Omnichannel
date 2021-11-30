namespace OpenStore.Omnichannel.Storefront.Models.Checkout;

public record OrderSummaryViewModel(bool AnyItem, decimal Total, decimal? DiscountPercentage, decimal? Discount, decimal Subtotal);