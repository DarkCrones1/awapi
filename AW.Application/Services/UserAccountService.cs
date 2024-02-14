using System.Linq.Expressions;
using AW.Common.Entities;
using AW.Common.Exceptions;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;
using AW.Domain.Interfaces;
using AW.Domain.Interfaces.Services;

namespace AW.Application.Services;

public class UserAccountService : CrudService<UserAccount>, IUserAccountService
{
    public UserAccountService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<int> CreateUser(UserAccount user)
    {
        Expression<Func<UserAccount, bool>> filter = x => x.UserName == user.UserName && !x.IsDeleted!.Value;

        var userAccount = await _unitOfWork.UserAccountRepository.Exist(filter);

        if (userAccount)
            throw new BusinessException("El usuario ya existe, intente con otro nombre de usuario");

        await _unitOfWork.UserAccountRepository.Create(user);

        await _unitOfWork.SaveChangesAsync();
        return user.Id;
    }

    public async Task<PagedList<UserAccount>> GetPaged(UserAccountQueryFilter filter)
    {
        var result = await _unitOfWork.UserAccountRepository.GetPaged(filter);
        var pagedItems = PagedList<UserAccount>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}