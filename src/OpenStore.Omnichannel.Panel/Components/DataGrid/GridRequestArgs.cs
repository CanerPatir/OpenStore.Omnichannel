namespace OpenStore.Omnichannel.Panel.Components.DataGrid
{
   
    public class GridRequestArgs
    {
        public int PageNr { get; }

        public int PageSize { get; }

        public string SortColumn { get; }

        public GridSortDirection SortDirection { get; }
        
        public string FilterColumn { get; }

        public string FilterTerm { get; set; }

        public GridRequestArgs(int pageNr, int pageSize, string sortColumn, string filterColumn, string filterTerm, GridSortDirection sortDirection)
        {
            PageNr = pageNr;
            PageSize = pageSize;
            SortColumn = sortColumn;
            FilterColumn = filterColumn;
            FilterTerm = filterTerm;
            SortDirection = sortDirection;
        }
    }
}