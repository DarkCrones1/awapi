using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class TechniqueType : CatalogBaseAuditablePaginationEntity
{
    public virtual ICollection<Craftman> Craftman { get; } = new List<Craftman>();

    public virtual ICollection<Craft> Craft { get; } = new List<Craft>();

    public virtual ICollection<TechniqueTypeProperty> Property { get; } = new List<TechniqueTypeProperty>();
}