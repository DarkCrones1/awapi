using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Sale : BaseRemovableAuditablePaginationEntity
{
    public int CraftmanId { get; set; }

    public DateTime SaleDate { get; set; }

    public int CustomerId { get; set; }

    public decimal Amount { get; set; }

    public int CraftId { get; set; }

    public virtual Craftman Craftman { get; set; } = null!;

    public virtual Craft Craft { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}