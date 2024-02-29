using Microsoft.EntityFrameworkCore;

using AW.Domain.Entities;
using AW.Domain.Interfaces.Repositories;
using AW.Infrastructure.Data;
using AW.Domain.Dto.QueryFilters;

namespace AW.Infrastructure.Repositories;

public class CraftmanRepository : CrudRepository<Craftman>, ICraftmantRepository
{
    public CraftmanRepository(AWDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Craftman>> GetPaged(Craftman entity)
    {
        var query = _dbContext.Craftman.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Craftman>> GetPaged(CraftmanQueryFilter entity)
    {
        var query = _dbContext.Craftman.AsQueryable();

        return await query.ToListAsync();
    }
}