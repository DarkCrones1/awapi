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

    public async Task<ActiveUserAccountCustomer> GetUserAccountCustomer(int id)
    {
        var entity = await _unitOfWork.UserAccountRepository.GetUserAccountCustomer(id);
        return entity;
    }

    public async Task<ActiveUserAccountCustomer> GetUserAccountCustomerToLogin(Expression<Func<ActiveUserAccountCustomer, bool>> filters)
    {
        var entity = await _unitOfWork.UserAccountRepository.GetUserAccountCustomerToLogin(filters);
        return entity;
    }

    public async Task<ActiveUserAccountCraftman> GetUserAccountCraftman(int id)
    {
        var entity = await _unitOfWork.UserAccountRepository.GetUserAccountCraftman(id);
        return entity;
    }

    public async Task<ActiveUserAccountCraftman> GetUserAccountCraftmanToLogin(Expression<Func<ActiveUserAccountCraftman, bool>> filters)
    {
        var entity = await _unitOfWork.UserAccountRepository.GetUserAccountCraftmanToLogin(filters);
        return entity;
    }

    public async Task<PagedList<UserAccount>> GetPagedCraftman(UserAccountQueryFilter filter)
    {
        var result = await _unitOfWork.UserAccountRepository.GetPagedCraftman(filter);
        var pagedItems = PagedList<UserAccount>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }

    public async Task<PagedList<UserAccount>> GetPagedCustomer(UserAccountQueryFilter filter)
    {
        var result = await _unitOfWork.UserAccountRepository.GetPagedCustomer(filter);
        var pagedItems = PagedList<UserAccount>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}