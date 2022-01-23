namespace OpenStore.Omnichannel.Domain.OrderContext;

public record OrderTimelineItem(DateTime UtcDate, string Description, OrderTimelineItemType Type);