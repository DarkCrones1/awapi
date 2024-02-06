using System.ComponentModel.DataAnnotations.Schema;
using AW.Common.Interfaces.Entities;

namespace AW.Common.Entities;
public abstract class BaseEntityPagination : BaseEntity, IPaginationQueryable
{
    [NotMapped]
    public int PageSize { get; set; }

    [NotMapped]
    public int PageNumber { get; set; }
}
