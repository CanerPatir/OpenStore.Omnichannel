using System.Collections.Generic;

namespace OpenStore.Omnichannel
{
    public class PagingMetaData
    {
        public int CurrentPage { get; set; }
        public int? TotalPages { get; set; }
        public int? PageSize { get; set; }
        public long TotalCount { get; set; }
    }

    public class PagedList<T>
    {
        public PagingMetaData PageMeta { get; set; }

        public IEnumerable<T> Items { get; set; }
    }

}