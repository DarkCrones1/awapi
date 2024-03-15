using AW.Common.Entities;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;
using AW.Domain.Interfaces;
using AW.Domain.Interfaces.Services;

namespace AW.Application.Services;

public class TicketService : CrudService<Ticket>, ITicketService
{
    public TicketService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Ticket>> GetPaged(TicketQueryFilter filter)
    {
        var result = await _unitOfWork.TicketRepository.GetPaged(filter);
        var pagedItems = PagedList<Ticket>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}