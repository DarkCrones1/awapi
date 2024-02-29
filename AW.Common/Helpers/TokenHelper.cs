using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace AW.Common.Helpers;

public class TokenHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenHelper(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    public string GetFullName()
    {
        var identity = _httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
        var userName = identity!.FindFirst(ClaimTypes.Name);

        return userName!.Value;
    }

    public string GetUserName()
    {
        var identity = _httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
        var nameIdentifier = identity!.FindFirst(ClaimTypes.NameIdentifier);
        
        return nameIdentifier!.Value;
    }

    public string GetAccountId()
    {
        var identity = _httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
        var accountId = identity!.FindFirst(ClaimTypes.Sid);

        return accountId!.Value;
    }

    public string GetUserAccountType()
    {
        var identity = _httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
        var accountType = identity!.FindFirst("UserAccountType");

        return accountType!.Value;
    }

    public string GetCustomerId()
    {
        var identity = _httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
        var customerId = identity!.FindFirst("CustomerId");

        return customerId!.Value;
    }

    public string GetCraftmanId()
    {
        var identity = _httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
        var craftmanId = identity!.FindFirst("CraftmanId");

        return craftmanId!.Value;
    }
}