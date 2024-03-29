using AW.Common.Interfaces.Entities;
using AW.Common.QueryFilters;

namespace AW.Domain.Dto.QueryFilters;

public class CustomerQueryFilter : PaginationControlRequestFilter, IBaseQueryFilter
{
    public int? Id { get; set; }

    public Guid? Code { get; set; }

    public string? Name { get; set; }

    public string? CellPhone { get; set; }

    public string? Phone { get; set; }

    public int CustomerTypeId { get; set; }

    public short Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? MinBirthDate { get; set; }

    public DateTime? MaxBirthDate { get; set; }

    public bool? IsDeleted { get; set; }
}