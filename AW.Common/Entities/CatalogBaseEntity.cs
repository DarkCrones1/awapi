using AW.Common.Interfaces.Entities;

namespace AW.Common.Entities;
public abstract class CatalogBaseEntity : BaseEntity, ICatalogBaseEntity, IRemovableEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool? IsDeleted { get; set; }
}