namespace OpenStore.Omnichannel.Shared.Dto.Management.Order;

public class OrderDto
{
    public Guid? Id { get; set; }
    public int? Number { get; set; }
    public OrderFinancialStatus FinancialStatus { get; protected set; }

    public string Notes { get; set; }
    public bool IsPreorder { get; set; }
    public DateTime ProcessedAt { get; set; }
    public DateTime ClosedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public string CancelReason { get; set; }
    public bool IsCancelled { get; set; }

    // Flags
    public bool IsPaid { get; init; }
    public bool IsFulfilled { get; init; }
    public bool IsPartiallyFulfilled { get; init; }
    public bool IsReturnInProgress { get; init; }
    public bool IsReturned { get; init; }
    public bool IsPartiallyReturned { get; init; }
}