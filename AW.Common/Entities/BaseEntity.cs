using AW.Common.Interfaces.Entities;

namespace AW.Common.Entities;

public abstract class BaseEntity : IBaseQueryable
{
    public int Id { get; set; }
}