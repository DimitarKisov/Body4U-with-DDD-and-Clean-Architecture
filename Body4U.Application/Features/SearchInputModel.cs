namespace Body4U.Application.Features
{
    public class SearchInputModel
    {
        public string? SortBy { get; set; }

        public string? OrderBy { get; set; }

        public int PageIndex { get; set; } = 0;

        public int PageSize { get; set; } = 10;
    }
}
