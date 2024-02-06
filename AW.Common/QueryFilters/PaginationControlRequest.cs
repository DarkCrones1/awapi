using AW.Common.Interfaces.Entities;

namespace AW.Common.QueryFilters;

public abstract class PaginationControlRequestFilter : IPaginationQueryable
{
    public int PageSize { get; set; } = 30;
    public int PageNumber { get; set; } = 1;
}