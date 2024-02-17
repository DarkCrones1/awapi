using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using AW.Common.Entities;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;
using AW.Domain.Interfaces;
using AW.Domain.Interfaces.Services;

namespace AW.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork unitOfWork;

    public AuthenticationService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> IsValidUser(string UserNameOrEmail, string password)
    {
        Expression<Func<UserAccount, bool>> filters = x =>
                (x.UserName == UserNameOrEmail || x.Email == UserNameOrEmail)
                && x.Password == password
                && x.IsActive
                && x.IsAuthorized
                && !x.IsDeleted!.Value;

        var result = await unitOfWork.UserAccountRepository.Exist(filters);

        return result;
    }
}