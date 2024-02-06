using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Category : CatalogBaseAuditablePaginationEntity
{
    public virtual ICollection<Craft> Craft { get; set; } = new List<Craft>();
}