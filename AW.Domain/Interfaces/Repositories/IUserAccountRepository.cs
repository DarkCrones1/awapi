using System.Linq.Expressions;

using AW.Common.Interfaces.Repositories;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;

namespace AW.Domain.Interfaces.Repositories;

public interface IUserAccountRepository : IQueryPagedRepository<ActiveUserAccountAdministrator>, ICrudRepository<UserAccount>, IQueryFilterPagedRepository<UserAccount, UserAccountQueryFilter>
{
    Task<ActiveUserAccountCustomer> GetUserAccountCustomer(int id);
    Task<ActiveUserAccountCustomer> GetUserAccountCustomerToLogin(Expression<Func<ActiveUserAccountCustomer, bool>> filters);
}