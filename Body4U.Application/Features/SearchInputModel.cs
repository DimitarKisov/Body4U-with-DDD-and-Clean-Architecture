namespace Body4U.Application.Features
{
    public class SearchInputModel
    {
        //public SearchInputModel(
        //    string? sortBy,
        //    string? orderBy,
        //    int pageIndex,
        //    int pageSize)
        //{
        //    this.SortBy = sortBy;
        //    this.OrderBy = orderBy;
        //    this.PageIndex = pageIndex;
        //    this.PageSize = pageSize;
        //}
        public string? SortBy { get; set; }

        public string? OrderBy { get; set; }

        public int PageIndex { get; set; } = 0;

        public int PageSize { get; set; } = 10;
    }
}
