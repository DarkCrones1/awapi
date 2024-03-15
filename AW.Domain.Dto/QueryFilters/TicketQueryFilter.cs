using AW.Common.Interfaces.Entities;
using AW.Common.QueryFilters;

namespace AW.Domain.Dto.QueryFilters;

public class TicketQueryFilter : PaginationControlRequestFilter, IBaseQueryFilter
{
    public int Id { get; set; }

    public Guid? SerialId { get; set; }

    public int CartId { get; set; }

    public int CustomerId { get; set; }

    public short Status { get; set; }

    public DateTime? CloseTicket { get; set; }

    public DateTime? MinCloseTicket { get; set; }

    public DateTime? MaxCloseTicket { get; set; }
}