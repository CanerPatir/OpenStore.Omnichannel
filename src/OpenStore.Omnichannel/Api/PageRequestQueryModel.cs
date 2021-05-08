namespace OpenStore.Omnichannel.Api
{
    /// <summary>
    /// temp type to handle query string params. We can not use PageRequest because of aspnet core not support immutable FromQuery Poco
    /// </summary>
    public class PageRequestQueryModel
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string SortColumn { get; set; }

        public SortDirection SortDirection { get; set; }

        public string FilterTerm { get; set; }

        public static implicit operator PageRequest(PageRequestQueryModel pageRequestQueryModel)
            => new(pageRequestQueryModel.PageNumber,
                pageRequestQueryModel.PageSize,
                pageRequestQueryModel.SortColumn,
                pageRequestQueryModel.FilterTerm,
                pageRequestQueryModel.SortDirection);

        public static explicit operator PageRequestQueryModel(PageRequest pageRequest) => new()
        {
            PageNumber = pageRequest.PageNumber,
            PageSize = pageRequest.PageSize,
            SortColumn = pageRequest.SortColumn,
            SortDirection = pageRequest.SortDirection,
            FilterTerm = pageRequest.FilterTerm
        };
    }
}