using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Culture : CatalogBaseAuditablePaginationEntity
{
    public virtual ICollection<Craft> Craft { get; } = new List<Craft>();
}