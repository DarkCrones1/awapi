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

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (entity.Code.HasValue)
            query = query.Where(x => x.Code == entity.Code);

        if (!string.IsNullOrEmpty(entity.Name) && !string.IsNullOrWhiteSpace(entity.Name))
            query = query.Where(x => x.FirstName.Contains(entity.Name) || x.LastName.Contains(entity.Name) || x.MiddleName!.Contains(entity.Name));

        if (!string.IsNullOrEmpty(entity.CellPhone) && !string.IsNullOrWhiteSpace(entity.CellPhone))
            query = query.Where(x => x.CellPhone.Contains(entity.CellPhone));

        if (!string.IsNullOrEmpty(entity.Phone) && !string.IsNullOrWhiteSpace(entity.Phone))
            query = query.Where(x => x.Phone!.Contains(entity.Phone));

        if (entity.Gender > 0)
            query = query.Where(x => x.Gender == entity.Gender);

        if (entity.BirthDate.HasValue)
            query = query.Where(x => x.BirthDate!.Value == entity.BirthDate.Value.Date);

        if (entity.MinBirthDate.HasValue)
            query = query.Where(x => x.BirthDate!.Value >= entity.MinBirthDate.Value.Date);

        if (entity.MaxBirthDate.HasValue)
            query = query.Where(x => x.BirthDate!.Value <= entity.MaxBirthDate.Value.Date);

        if (entity.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted!.Value == entity.IsDeleted.Value);

        if (entity.Status > 0)
            query = query.Where(x => x.Status == entity.Status);

        return await query.ToListAsync();
    }
}