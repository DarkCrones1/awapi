namespace AW.Domain.Interfaces.Services;

public interface IAuthenticationService
{
    Task<bool> IsValidUser(string UserNameOrEmail, string password);
}