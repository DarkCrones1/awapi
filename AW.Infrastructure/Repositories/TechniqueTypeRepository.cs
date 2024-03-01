using Microsoft.EntityFrameworkCore;

using AW.Domain.Entities;
using AW.Domain.Interfaces.Repositories;
using AW.Infrastructure.Data;
using AW.Domain.Dto.QueryFilters;

namespace AW.Infrastructure.Repositories;

public class TechniqueTypeRepository : CatalogBaseRepository<TechniqueType>, ITechniqueTypeRepository
{
    public TechniqueTypeRepository(AWDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<TechniqueType>> GetPaged(TechniqueType entity)
    {
        var query = _dbContext.TechniqueType.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TechniqueType>> GetPaged(TechniqueQueryFilter entity)
    {
        var query = _dbContext.TechniqueType.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (!string.IsNullOrEmpty(entity.Name) && !string.IsNullOrWhiteSpace(entity.Name))
            query = query.Where(x => x.Name.Contains(entity.Name));

        if (!string.IsNullOrEmpty(entity.Description) && !string.IsNullOrWhiteSpace(entity.Description))
            query = query.Where(x => x.Description!.Contains(entity.Description));

        if (entity.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == entity.IsDeleted);

        return await query.ToListAsync();
    }
}