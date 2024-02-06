using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class CraftFamily : CatalogBaseAuditablePaginationEntity
{
    public int? ParentId { get; set; }

    public bool IsJewel { get; set; }

    public virtual Craft Craft { get; set; } = null!;

    public virtual CraftFamily? Parent { get; set; }
}