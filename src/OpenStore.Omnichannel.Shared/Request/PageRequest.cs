// ReSharper disable CheckNamespace

using System.Text.Json.Serialization;

namespace OpenStore.Omnichannel;

public class PageRequest
{
    public int PageNumber { get; }

    public int PageSize { get; }

    public string SortColumn { get; }

    public SortDirection SortDirection { get; }

    public string FilterTerm { get; }

    public PageRequest(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    [JsonConstructor]
    public PageRequest(int pageNumber, int pageSize, string sortColumn, string filterTerm, SortDirection sortDirection) : this(pageNumber, pageSize)
    {
        SortColumn = sortColumn;
        FilterTerm = filterTerm;
        SortDirection = sortDirection;
    }
}