using AW.Common.Interfaces.Entities;

namespace AW.Common.Interfaces.Entities;

public interface ICatalogBaseEntity : IBaseQueryable
{
    public string Name { get; set; }

    public string? Description { get; set; }
}