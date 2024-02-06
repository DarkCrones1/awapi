using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Cart : BaseAuditablePaginationEntity
{
    public int CustomerId { get; set; }

    public short Status { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Craft> Craft { get; } = new List<Craft>();

    public virtual Ticket Ticket { get; set; } = null!;
}