using System.Linq.Expressions;
using AW.Common.Entities;
using AW.Common.Interfaces.Services;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;

namespace AW.Domain.Interfaces.Services;

public interface IUserAccountService : ICrudService<UserAccount>
{
    Task<int> CreateUser(UserAccount user);
    Task<PagedList<UserAccount>> GetPaged(UserAccountQueryFilter filter);
}