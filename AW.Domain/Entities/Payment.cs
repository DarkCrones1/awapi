using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Payment : BaseRemovableAuditablePaginationEntity
{
    public Guid SerialId { get; set; }

    public int TicketId { get; set; }

    public decimal AmountPay { get; set; }

    public short Status { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;
}