using System.Linq.Expressions;
using AW.Common.Entities;
using AW.Common.Interfaces.Services;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;

namespace AW.Domain.Interfaces.Services;

public interface IUserAccountService : ICrudService<UserAccount>
{
    Task<int> CreateUser(UserAccount user);

    Task<ActiveUserAccount> GetUserAccount(int id);
    Task<ActiveUserAccount> GetUserAccountToLogin(Expression<Func<ActiveUserAccount, bool>> filters);

    Task<ActiveUserAccountCustomer> GetUserAccountCustomer(int id);
    Task<ActiveUserAccountCustomer> GetUserAccountCustomerToLogin(Expression<Func<ActiveUserAccountCustomer, bool>> filters);

    Task<ActiveUserAccountCraftman> GetUserAccountCraftman(int id);
    Task<ActiveUserAccountCraftman> GetUserAccountCraftmanToLogin(Expression<Func<ActiveUserAccountCraftman, bool>> filters);

    Task<PagedList<UserAccount>> GetPagedCraftman(UserAccountQueryFilter filter);
    Task<PagedList<UserAccount>> GetPagedCustomer(UserAccountQueryFilter filter);
}