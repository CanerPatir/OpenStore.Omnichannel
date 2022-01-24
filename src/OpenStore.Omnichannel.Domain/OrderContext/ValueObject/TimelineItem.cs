namespace OpenStore.Omnichannel.Domain.OrderContext;

public record TimelineItem(DateTime UtcDate, string Description, TimelineItemType Type);