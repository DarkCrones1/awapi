using AW.Common.Entities;

namespace AW.Domain.Entities;

public class Property : CatalogBaseAuditablePaginationEntity
{
    public short PropertyType { get; set; }

    public virtual ICollection<CraftProperty> CraftProperty { get; } = new List<CraftProperty>();

    public virtual ICollection<TechniqueTypeProperty> TechniqueTypeProperty { get; } = new List<TechniqueTypeProperty>();
}