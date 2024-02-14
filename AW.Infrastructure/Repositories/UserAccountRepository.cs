using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using AW.Domain.Entities;
using AW.Domain.Interfaces.Repositories;
using AW.Infrastructure.Data;
using AW.Domain.Dto.QueryFilters;

namespace AW.Infrastructure.Repositories;

public class UserAccountRepository : CrudRepository<UserAccount>, IUserAccountRepository
{
    public UserAccountRepository(AWDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<ActiveUserAccountAdministrator>> GetPaged(ActiveUserAccountAdministrator entity)
    {
        var query = _dbContext.ActiveUserAccountAdministrator.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<UserAccount>> GetPaged(UserAccountQueryFilter entity)
    {
        var query = _dbContext.UserAccount.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (!string.IsNullOrEmpty(entity.UserName) && !string.IsNullOrWhiteSpace(entity.UserName))
            query = query.Where(x => x.UserName.Contains(entity.UserName));

        if (entity.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == entity.IsDeleted);

        query = query.Where(x => x.AccountType == 1 || x.AccountType == 2);

        return await query.ToListAsync();
    }

    public async Task<ActiveUserAccountCustomer> GetUserAccountCustomer(int id)
    {
        Expression<Func<ActiveUserAccountCustomer, bool>> filter = x => x.Id == id;
        var entity = await GetUserAccountCustomerToLogin(filter);

        return entity ?? new ActiveUserAccountCustomer();
    }

    public async Task<ActiveUserAccountCustomer> GetUserAccountCustomerToLogin(Expression<Func<ActiveUserAccountCustomer, bool>> filters)
    {
        var entity = await _dbContext.ActiveUserAccountCustomer
        .Where(filters)
        .AsNoTracking()
        .FirstOrDefaultAsync();

        return entity ?? new ActiveUserAccountCustomer();
    }
}