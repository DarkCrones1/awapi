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

    public string GetName()
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

        // if (nameIdentifier != null)
        // {
        //     return nameIdentifier.Value;
        // }
        // else
        // {
        //     return "Admin";
        // }
    }
}