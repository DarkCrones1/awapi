using System.Linq.Expressions;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using AW.Common.Interfaces.Services;
using AW.Domain.Dto.Response;
using AW.Domain.Entities;
using AW.Api.Responses;
using AW.Domain.Dto.Request.Create;
// using AW.Domain.Dto.Request.Update;
using AW.Common.Exceptions;
using AW.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Interfaces.Services;
using AW.Common.Functions;

namespace Aw.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class UserAccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserAccountService _service;
    private readonly ICatalogBaseService<Rol> _rolService;
    private readonly TokenHelper _tokenHelper;

    public UserAccountController(IMapper mapper, IUserAccountService service, ICatalogBaseService<Rol> rolService, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._rolService = rolService;
        this._tokenHelper = tokenHelper;
    }

    [HttpPost]
    [Route("Customer")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UserAccountCustomerResponseDto>))]
    public async Task<IActionResult> CreateUserAccountCustomer([FromBody] UserAccountCustomerCreateRequestDto requestDto)
    {
        Expression<Func<UserAccount, bool>> filter = x => !x.IsDeleted!.Value && x.Email == requestDto.Email;

        var existUser = await _service.Exist(filter);

        if (existUser)
            return BadRequest("Ya existe un usuario con este correo electrónico");

        var entity = await PopulateUserAccountCustomer(requestDto);
        entity.Password = MD5Encrypt.GetMD5(requestDto.Password);
        await _service.CreateUser(entity);

        var result = _mapper.Map<UserAccountCustomerResponseDto>(entity);
        var response = new ApiResponse<UserAccountCustomerResponseDto>(result);
        return Ok(response);
    }

    private async Task<UserAccount> PopulateUserAccountCustomer(UserAccountCustomerCreateRequestDto requestDto)
    {
        Expression<Func<UserAccount, bool>> filter = x => !x.IsDeleted!.Value && x.Email == requestDto.Email;

        var existUser = await _service.Exist(filter);
            
        var userAccount = _mapper.Map<UserAccount>(requestDto);

        var customer = _mapper.Map<Customer>(requestDto);
        customer.UserAccount.Add(userAccount);

        userAccount.Customer.Add(customer);

        return userAccount;
    }

}