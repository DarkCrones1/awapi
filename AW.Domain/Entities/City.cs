using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class City : BaseRemovableAuditablePaginationEntity
{
    public string Name { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string? PhoneCode { get; set; }

    public int? CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Craft> Craft { get; } = new List<Craft>();

    public virtual ICollection<Craftman> Craftman { get; } = new List<Craftman>();
}