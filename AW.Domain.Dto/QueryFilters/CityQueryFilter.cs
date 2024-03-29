using AW.Common.Interfaces.Entities;
using AW.Common.QueryFilters;

namespace AW.Domain.Dto.QueryFilters;

public class CityQueryFilter : PaginationControlRequestFilter, IBaseQueryFilter
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ZipCode { get; set; }

    public string? PhoneCode { get; set; }

    public int CountryId { get; set; }
}