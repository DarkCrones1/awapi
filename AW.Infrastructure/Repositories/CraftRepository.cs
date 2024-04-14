using Microsoft.EntityFrameworkCore;

using AW.Domain.Entities;
using AW.Domain.Interfaces.Repositories;
using AW.Infrastructure.Data;
using AW.Domain.Dto.QueryFilters;

namespace AW.Infrastructure.Repositories;

public class CraftRepository : CatalogBaseRepository<Craft>, ICraftRepository
{
    public CraftRepository(AWDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Craft>> GetPaged(Craft entity)
    {
        var query = _dbContext.Craft.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Craft>> GetPaged(CraftQueryFilter entity)
    {
        var query = _dbContext.Craft.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (!string.IsNullOrEmpty(entity.Name) && !string.IsNullOrWhiteSpace(entity.Name))
            query = query.Where(x => x.Name.Contains(entity.Name));

        if (!string.IsNullOrEmpty(entity.Description) && !string.IsNullOrWhiteSpace(entity.Description))
            query = query.Where(x => x.Description!.Contains(entity.Description));

        if (entity.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == entity.IsDeleted);

        if (entity.SerialId.HasValue)
            query = query.Where(x => x.SerialId == entity.SerialId);

        if (!string.IsNullOrEmpty(entity.InitialPartSerialId) && !string.IsNullOrWhiteSpace(entity.InitialPartSerialId))
            query = query.Where(x => x.SerialId.ToString().StartsWith(entity.InitialPartSerialId));

        if (entity.CraftmanId > 0)
            query = query.Where(x => x.CraftmanId == entity.CraftmanId);

        if (!string.IsNullOrEmpty(entity.CraftmanName) && !string.IsNullOrWhiteSpace(entity.CraftmanName))
            query = query.Where(x => x.Craftman.FirstName.Contains(entity.CraftmanName) || x.Craftman.LastName.Contains(entity.CraftmanName) || x.Craftman.MiddleName!.Contains(entity.CraftmanName));

        if (entity.CraftStatus > 0)
            query = query.Where(x => x.Status == entity.CraftStatus);

        if (entity.CultureId > 0)
            query = query.Where(x => x.CultureId == entity.CultureId);

        if (entity.PublicationDate.HasValue)
            query = query.Where(x => x.PublicationDate == entity.PublicationDate.Value.Date);

        if (entity.PublicationDate.HasValue)
            query = query.Where(x => x.PublicationDate!.Value >= entity.MinPublicationDate!.Value.Date);

        if (entity.PublicationDate.HasValue)
            query = query.Where(x => x.PublicationDate!.Value >= entity.MaxPublicationDate!.Value.Date);

        if (entity.CreatedDate.HasValue)
            query = query.Where(x => x.CreatedDate.Date == entity.CreatedDate.Value.Date);

        if (entity.MinCreatedDate.HasValue)
            query = query.Where(x => x.CreatedDate.Date >= entity.MinCreatedDate.Value.Date);

        if (entity.MaxCreatedDate.HasValue)
            query = query.Where(x => x.CreatedDate.Date <= entity.MaxCreatedDate.Value.Date);

        if (entity.Price > 0)
            query = query.Where(x => x.Price == entity.Price);

        if (entity.MinPrice > 0)
            query = query.Where(x => x.Price >= entity.MinPrice);

        if (entity.MaxPrice > 0)
            query = query.Where(x => x.Price <= entity.MaxPrice);

        if (entity.CategoryIds != null && entity.CategoryIds.Length > 0)
            query = query.Where(x => x.Category.Any(x => entity.CategoryIds.Contains(x.Id)));

        if (entity.TechniqueTypeIds != null && entity.TechniqueTypeIds.Length > 0)
            query = query.Where(x => x.TechniqueType.Any(x => entity.TechniqueTypeIds.Contains(x.Id)));

        return await query.ToListAsync();
    }
}