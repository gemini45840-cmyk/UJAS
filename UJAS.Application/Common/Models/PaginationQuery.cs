namespace UJAS.Application.Common.Models
{
    public abstract class PaginationQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; }
        public bool SortDescending { get; set; } = false;
        public string SearchTerm { get; set; }

        public virtual int Skip => (PageNumber - 1) * PageSize;
        public virtual int Take => PageSize;
    }

    public class FilterCriteria
    {
        public string Field { get; set; }
        public string Operator { get; set; } // eq, ne, gt, lt, contains, etc.
        public object Value { get; set; }
    }

    public class AdvancedFilterQuery : PaginationQuery
    {
        public List<FilterCriteria> Filters { get; set; } = new();
        public Dictionary<string, string> Includes { get; set; } = new();
    }
}