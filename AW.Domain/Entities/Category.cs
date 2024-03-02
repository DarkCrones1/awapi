using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Category : CatalogBaseAuditablePaginationEntity
{
    public string? CategoryPictureUrl { get; set; }

    public virtual ICollection<Craft> Craft { get; set; } = new List<Craft>();
}