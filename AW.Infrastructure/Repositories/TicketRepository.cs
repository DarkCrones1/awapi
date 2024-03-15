using Microsoft.EntityFrameworkCore;

using AW.Domain.Entities;
using AW.Domain.Interfaces.Repositories;
using AW.Infrastructure.Data;
using AW.Domain.Dto.QueryFilters;

namespace AW.Infrastructure.Repositories;

public class TicketRepository : CrudRepository<Ticket>, ITicketRepository
{
    public TicketRepository(AWDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Ticket>> GetPaged(Ticket entity)
    {
        var query = _dbContext.Ticket.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetPaged(TicketQueryFilter entity)
    {
        var query = _dbContext.Ticket.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (entity.SerialId.HasValue)
            if (entity.SerialId != Guid.Empty)
                query = query.Where(x => x.SerialId == entity.SerialId);

        if (entity.CartId > 0)
            query = query.Where(x => x.CartId == entity.CartId);

        if (entity.CustomerId > 0)
            query = query.Where(x => x.CustomerId == entity.CustomerId);

        if (entity.Status > 0)
            query = query.Where(x => x.Status == entity.Status);

        if (entity.CloseTicket.HasValue)
            query = query.Where(x => x.CloseTicket!.Value == entity.CloseTicket.Value);

        if (entity.MinCloseTicket.HasValue)
            query = query.Where(x => x.CloseTicket!.Value >= entity.MinCloseTicket.Value);

        if (entity.MaxCloseTicket.HasValue)
            query = query.Where(x => x.CloseTicket!.Value <= entity.MaxCloseTicket.Value);

        return await query.ToListAsync();
    }
}