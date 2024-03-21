using System.Linq.Expressions;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using AW.Common.Interfaces.Services;
using AW.Domain.Dto.Response;
using AW.Domain.Entities;
using AW.Api.Responses;
using AW.Domain.Dto.Request.Create;
using AW.Domain.Dto.Request.Update;
using AW.Common.Exceptions;
using AW.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Interfaces.Services;
using AW.Common.Functions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
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

    /// <summary>
    /// Devuelve los Artesanos mediante filtros y paginado
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Craftman")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UserAccountCraftmanResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<UserAccountCraftmanResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<UserAccountCraftmanResponseDto>>))]
    public async Task<IActionResult> GetUserCraftman([FromQuery] UserAccountQueryFilter filter)
    {
        var entities = await _service.GetPagedCraftman(filter);
        var dtos = _mapper.Map<IEnumerable<UserAccountCraftmanResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<UserAccountCraftmanResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    /// <summary>
    /// Devuelve los Clientes mediante filtros y paginado
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("Customer")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UserAccountCustomerResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<UserAccountCustomerResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<UserAccountCustomerResponseDto>>))]
    public async Task<IActionResult> GetUserCustomer([FromQuery] UserAccountQueryFilter filter)
    {
        var entities = await _service.GetPagedCustomer(filter);
        var dtos = _mapper.Map<IEnumerable<UserAccountCustomerResponseDto>>(entities);

        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<UserAccountCustomerResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    /// <summary>
    /// Crea Cuentas de usuario para Artesanos
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Craftman")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UserAccountCraftmanResponseDto>))]
    public async Task<IActionResult> CreateUserAccountCraftmant([FromBody] UserAccountCraftmanCreateRequestDto requestDto)
    {
        try
        {
            Expression<Func<UserAccount, bool>> filterUserName = x => !x.IsDeleted!.Value && x.UserName == requestDto.UserName;

            var existUser = await _service.Exist(filterUserName);

            if (existUser)
                return BadRequest("Ya existe un perfil con este nombre de usuario");

            Expression<Func<UserAccount, bool>> filterEmail = x => !x.IsDeleted!.Value && x.Email == requestDto.Email;

            var existEmail = await _service.Exist(filterEmail);

            if (existUser)
                return BadRequest("Ya existe un usuario con este correo electrónico");

            var entity = await PopulateUserAccountCraftman(requestDto);
            entity.Password = MD5Encrypt.GetMD5(requestDto.Password);
            await _service.CreateUser(entity);

            var result = _mapper.Map<UserAccountCraftmanResponseDto>(entity);
            var response = new ApiResponse<UserAccountCraftmanResponseDto>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    /// <summary>
    /// Crea cuentas de usuario para Clientes
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Customer")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UserAccountCustomerResponseDto>))]
    public async Task<IActionResult> CreateUserAccountCustomer([FromBody] UserAccountCustomerCreateRequestDto requestDto)
    {
        try
        {
            Expression<Func<UserAccount, bool>> filterUserName = x => !x.IsDeleted!.Value && x.UserName == requestDto.UserName;

            var existUser = await _service.Exist(filterUserName);

            if (existUser)
                return BadRequest("Ya existe un perfil con este nombre de usuario");

            Expression<Func<UserAccount, bool>> filterEmail = x => !x.IsDeleted!.Value && x.Email == requestDto.Email;

            var existEmail = await _service.Exist(filterEmail);

            if (existUser)
                return BadRequest("Ya existe un usuario con este correo electrónico");

            var entity = await PopulateUserAccountCustomer(requestDto);
            entity.Password = MD5Encrypt.GetMD5(requestDto.Password);
            await _service.CreateUser(entity);

            var result = _mapper.Map<UserAccountCustomerResponseDto>(entity);
            var response = new ApiResponse<UserAccountCustomerResponseDto>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    private async Task<UserAccount> PopulateUserAccountCraftman(UserAccountCraftmanCreateRequestDto requestDto)
    {
        Expression<Func<UserAccount, bool>> filter = x => !x.IsDeleted!.Value && x.Email == requestDto.Email;

        var existUser = await _service.Exist(filter);

        var userAccount = _mapper.Map<UserAccount>(requestDto);

        var craftman = _mapper.Map<Craftman>(requestDto);
        craftman.UserAccount.Add(userAccount);

        userAccount.Craftman.Add(craftman);

        return userAccount;
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

    /// <summary>
    /// Actualiza los datos de inicio de sesión
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, UserAccountUpdateRequestDto requestDto)
    {
        Expression<Func<UserAccount, bool>> filter = x => x.Id == id;

        var existUser = await _service.Exist(filter);

        if (!existUser)
            return BadRequest("No se encontró el usuario con esas características");

        var entity = await _service.GetById(id);
        entity.Id = id;
        entity.Password = MD5Encrypt.GetMD5(requestDto.Password);
        entity.IsDeleted = false;

        await _service.Update(entity);
        return Ok(true);
    }

    /// <summary>
    /// Borra de manera lógica una cuenta de usuario
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            Expression<Func<UserAccount, bool>> filter = x => x.Id == id;

            var existUserAccount = await _service.Exist(filter);

            if (!existUserAccount)
                return BadRequest("No existe el usuario que intenta eliminar");

            var newEntity = await _service.GetById(id);
            newEntity.IsDeleted = true;
            newEntity.IsActive = false;
            await _service.Update(newEntity);
            return Ok(true);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

}