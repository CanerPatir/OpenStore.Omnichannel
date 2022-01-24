namespace OpenStore.Omnichannel.Domain.OrderContext;

public record Discount(string Reason, string Code, DiscountType Type, decimal Amount);