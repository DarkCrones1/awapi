using AW.Common.Interfaces.Entities;
using AW.Common.QueryFilters;

namespace AW.Domain.Dto.QueryFilters;

public class CartQueryFilter : PaginationControlRequestFilter, IBaseQueryFilter
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public short Status { get; set; }

    public decimal Total { get; set; }

    public decimal MinTotal { get; set; }

    public decimal MaxTotal { get; set; }
}