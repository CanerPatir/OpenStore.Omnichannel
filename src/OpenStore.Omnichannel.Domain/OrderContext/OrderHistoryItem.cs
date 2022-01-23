namespace OpenStore.Omnichannel.Domain.OrderContext;

public record OrderHistoryItem(DateTime Date, string Description, OrderHistoryItemType Type);