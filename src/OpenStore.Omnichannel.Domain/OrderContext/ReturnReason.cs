namespace OpenStore.Omnichannel.Domain.OrderContext;

public enum ReturnReason
{
    SizeTooSmall,
    SizeTooLarge,
    CustomerChangedTheirMind,
    ItemNotAsDescribed,
    ReceivedWrongItem,
    DamagedOrDefective,
    Style,
    Color,
    Other
}