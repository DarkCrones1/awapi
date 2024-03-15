using AW.Common.Entities;
using AW.Common.Interfaces.Services;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;

namespace AW.Domain.Interfaces.Services;

public interface ITicketService : ICrudService<Ticket>
{
    Task<PagedList<Ticket>> GetPaged(TicketQueryFilter filter);
}