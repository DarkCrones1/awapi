using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Country : BaseRemovableAuditablePaginationEntity
{
    public string Name { get; set; } = null!;

    public string? Code { get; set; }

    public virtual ICollection<City> City { get; } = new List<City>();
}