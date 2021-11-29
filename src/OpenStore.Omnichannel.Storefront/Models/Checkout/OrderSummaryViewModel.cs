namespace OpenStore.Omnichannel.Storefront.Models.Checkout;

public record OrderSummaryViewModel(decimal Total, decimal? DiscountPercentage, decimal? Discount, decimal Subtotal);