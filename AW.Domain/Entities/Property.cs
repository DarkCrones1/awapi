using AW.Common.Entities;

namespace AW.Domain.Entities;

public class Property : CatalogBaseAuditablePaginationEntity
{
    public virtual ICollection<CraftProperty> CraftProperty { get; } = new List<CraftProperty>();

    public virtual ICollection<TechniqueTypeProperty> TechniqueTypePropertyc { get; } = new List<TechniqueTypeProperty>();
}