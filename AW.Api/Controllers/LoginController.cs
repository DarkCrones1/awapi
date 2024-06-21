using System.Linq.Expressions;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using AW.Common.Interfaces.Services;
using AW.Domain.Dto.Response;
using AW.Domain.Entities;
using AW.Api.Responses;
using AW.Domain.Dto.Request.Create;
using AW.Common.Exceptions;
using AW.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Interfaces.Services;
using AW.Domain.Dto.Request;
using AW.Api.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using AW.Common.Functions;

namespace AW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LoginController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _service;
    private readonly IUserAccountService _userAccountService;
    private readonly IConfiguration _configuration;
    protected ActiveUserAccountCraftman _userCraftman;
    protected ActiveUserAccountCustomer _userCustomer;
    protected ActiveUserAccountAdministrator _userAdmin;

    public LoginController(IMapper mapper, IAuthenticationService service, IUserAccountService userAccountService, IConfiguration configuration)
    {
        this._mapper = mapper;
        this._service = service;
        this._userAccountService = userAccountService;
        this._configuration = configuration;
        _userCraftman = new ActiveUserAccountCraftman();
        _userCustomer = new ActiveUserAccountCustomer();
        _userAdmin = new ActiveUserAccountAdministrator();
        SettingConfigurationFile.Initialize(_configuration);
    }

    /// <summary>
    /// Inicia Sesion, Login general
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> SignIn([FromBody] LoginRequestDto requestDto)
    {
        try
        {
            var result = await _service.IsValidUser(requestDto.UserNameOrEmail!, MD5Encrypt.GetMD5(requestDto.Password!));

            if (!result)
                return NotFound("El Usuario no es válido, revise que el Usuario/Email o la Contraseña sean correctos");

            _userCraftman = await GetCraftman(requestDto);
            if (_userCraftman.AccountType == 2)
            {
                // var dbConection = _configuration["UserConnection:awDevStringCraftman"];
                // _configuration["ConnectionStrings:awDevString"] = dbConection;
                var token = await GenerateTokenCraftman();
                return Ok(new { token });
            }

            _userCustomer = await GetCustomer(requestDto);
            if (_userCustomer.AccountType == 3)
            {
                // var dbConection = _configuration["UserConnection:awDevStringCustomer"];
                // _configuration["ConnectionStrings:awDevString"] = dbConection;
                var token = await GenerateTokenCustomer();
                return Ok(new { token });
            }

            return null!;
        }
        catch (Exception)
        {

            throw new LogicBusinessException("No se ha encontrado ningun usuario");
        }
    }

    private async Task<string> GenerateTokenCraftman()
    {
        // Header
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(signingCredentials);

        var lstClaims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, _userCraftman!.UserName),
                new Claim(ClaimTypes.Name, _userCraftman!.Name),
                new Claim(ClaimTypes.Email, _userCraftman.Email),
                new Claim(ClaimTypes.Sid, $"{_userCraftman.Id}"),
                new Claim(ClaimTypes.DateOfBirth, DateTime.Now.ToString()),
                //new Claim("", "") //TODO: agregar valores personalizados
                new Claim("UserAccountType", $"{_userCraftman.AccountType}"),
                new Claim("CraftmanId", $"{_userCraftman.CraftmanId}")
            };

        // Payload
        var elapsedTime = int.Parse(_configuration["Authentication:ExpirationMinutes"]!);

        var payload = new JwtPayload(
            issuer: _configuration["Authentication:Issuer"],
            audience: _configuration["Authentication:Audience"],
            claims: lstClaims,
            notBefore: DateTime.Now,
            expires: DateTime.UtcNow.AddMinutes(elapsedTime)
        );

        // Signature
        var token = new JwtSecurityToken(header, payload);

        await Task.CompletedTask;

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<string> GenerateTokenCustomer()
    {
        // Header
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(signingCredentials);

        var lstClaims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, _userCustomer!.UserName),
                new Claim(ClaimTypes.Name, _userCustomer!.Name),
                new Claim(ClaimTypes.Email, _userCustomer.Email),
                new Claim(ClaimTypes.Sid, $"{_userCustomer.Id}"),
                new Claim(ClaimTypes.DateOfBirth, DateTime.Now.ToString()),
                //new Claim("", "") //TODO: agregar valores personalizados
                new Claim("UserAccountType", $"{_userCustomer.AccountType}"),
                new Claim("CustomerId", $"{_userCustomer.CustomerId}")
            };

        // Payload
        var elapsedTime = int.Parse(_configuration["Authentication:ExpirationMinutes"]!);

        var payload = new JwtPayload(
            issuer: _configuration["Authentication:Issuer"],
            audience: _configuration["Authentication:Audience"],
            claims: lstClaims,
            notBefore: DateTime.Now,
            expires: DateTime.UtcNow.AddMinutes(elapsedTime)
        );

        // Signature
        var token = new JwtSecurityToken(header, payload);

        await Task.CompletedTask;

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<ActiveUserAccountCraftman> GetCraftman(LoginRequestDto requestDto)
    {
        Expression<Func<ActiveUserAccountCraftman, bool>> filters = x => x.UserName == requestDto.UserNameOrEmail || x.Email == requestDto.UserNameOrEmail;
        var entity = await _userAccountService.GetUserAccountCraftmanToLogin(filters);
        return entity;
    }

    private async Task<ActiveUserAccountCustomer> GetCustomer(LoginRequestDto requestDto)
    {
        Expression<Func<ActiveUserAccountCustomer, bool>> filters = x => x.UserName == requestDto.UserNameOrEmail || x.Email == requestDto.UserNameOrEmail;
        var entity = await _userAccountService.GetUserAccountCustomerToLogin(filters);
        return entity;
    }
}