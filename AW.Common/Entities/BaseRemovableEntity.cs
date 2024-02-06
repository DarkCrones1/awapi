using AW.Common.Interfaces.Entities;

namespace AW.Common.Entities;

public abstract class BaseRemovableEntity : BaseEntity, IRemovableEntity
{
    public bool? IsDeleted { get; set; }
}