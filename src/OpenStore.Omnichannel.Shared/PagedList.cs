using System.Collections.Generic;

// ReSharper disable CheckNamespace

namespace OpenStore.Omnichannel;

public class PagingMetaDataDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public long TotalCount { get; set; }
}

public class PagedListDto<T>
{
    public PagingMetaDataDto PageMeta { get; set; }

    public IEnumerable<T> Items { get; set; }
}