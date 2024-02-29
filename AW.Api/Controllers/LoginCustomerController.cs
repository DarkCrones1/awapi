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
public class LoginCustomerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _service;
    private readonly IUserAccountService _userAccountService;
    private readonly IConfiguration _configuration;
    protected ActiveUserAccountCustomer _user;

    public LoginCustomerController(IMapper mapper, IAuthenticationService service, IUserAccountService userAccountService, IConfiguration configuration)
    {
        this._mapper = mapper;
        this._service = service;
        this._userAccountService = userAccountService;
        this._configuration = configuration;
        _user = new ActiveUserAccountCustomer();
        SettingConfigurationFile.Initialize(_configuration);
    }

    /// <summary>
    /// Inicio de sesión para cuentas de Clientes
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPost]
    [Route("Customer")]
    public async Task<IActionResult> SignInCustomer([FromBody] LoginRequestDto requestDto)
    {
        try
        {
            var result = await _service.IsValidUser(requestDto.UserNameOrEmail!, MD5Encrypt.GetMD5(requestDto.Password!));

            if (!result)
                return NotFound("El Usuario no es válido, revise que el Usuario/Email o la Contraseña sean correctos");

            _user = await GetCustomer(requestDto);

            var token = await GenerateToken();
            return Ok(new { token });
        }
        catch (Exception)
        {

            throw new LogicBusinessException("Esta Cuenta de Usuario debe de iniciar como Artesano");
        }
    }

    private async Task<string> GenerateToken()
    {
        // Header
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(signingCredentials);

        var lstClaims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, _user!.UserName),
                new Claim(ClaimTypes.Name, _user!.Name),
                new Claim(ClaimTypes.Email, _user.Email),
                new Claim(ClaimTypes.Sid, $"{_user.Id}"),
                new Claim(ClaimTypes.DateOfBirth, DateTime.Now.ToString()),
                //new Claim("", "") //TODO: agregar valores personalizados
                new Claim("UserAccountType", $"{_user.AccountType}"),
                new Claim("CustomerId", $"{_user.CustomerId}")
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

    private async Task<ActiveUserAccountCustomer> GetCustomer(LoginRequestDto requestDto)
    {
        Expression<Func<ActiveUserAccountCustomer, bool>> filters = x => x.UserName == requestDto.UserNameOrEmail || x.Email == requestDto.UserNameOrEmail;
        var entity = await _userAccountService.GetUserAccountCustomerToLogin(filters);
        return entity;
    }
}