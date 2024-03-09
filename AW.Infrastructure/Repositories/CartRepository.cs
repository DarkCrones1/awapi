using Microsoft.EntityFrameworkCore;

using AW.Domain.Entities;
using AW.Domain.Interfaces.Repositories;
using AW.Infrastructure.Data;
using AW.Domain.Dto.QueryFilters;

namespace AW.Infrastructure.Repositories;

public class CartRepository : CrudRepository<Cart>, ICartRepository
{
    public CartRepository(AWDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Cart>> GetPaged(Cart entity)
    {
        var query = _dbContext.Cart.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Cart>> GetPaged(CartQueryFilter entity)
    {
        var query = _dbContext.Cart.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (entity.CustomerId > 0)
            query = query.Where(x => x.CustomerId == entity.CustomerId);

        if (entity.Status > 0)
            query = query.Where(x => x.Status == entity.Status);

        if (entity.Total > 0)
            query = query.Where(x => x.Total == entity.Total);

        if (entity.MinTotal > 0)
            query = query.Where(x => x.Total >= entity.MinTotal);

        if (entity.MaxTotal > 0)
            query = query.Where(x => x.Total <= entity.MaxTotal);

        return await query.ToListAsync();
    }
}