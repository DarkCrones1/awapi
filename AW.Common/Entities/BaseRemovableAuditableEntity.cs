using AW.Common.Interfaces.Entities;

namespace AW.Common.Entities;

public abstract class BaseRemovableAuditableEntity : BaseAuditableEntity, IRemovableEntity
{
    public bool? IsDeleted { get; set; }
}