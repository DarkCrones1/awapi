using AW.Common.Entities;

namespace AW.Domain.Entities;

public class Property : CatalogBaseAuditablePaginationEntity
{
    public short PropertyType { get; set; }

    public virtual ICollection<CraftProperty> Craft { get; } = new List<CraftProperty>();

    public virtual ICollection<TechniqueTypeProperty> TechniqueType { get; } = new List<TechniqueTypeProperty>();
}