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
using AW.Common.Enumerations;
using AW.Domain.Enumerations;

namespace AW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CartController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICartService _service;
    private readonly TokenHelper _tokenHelper;

    public CartController(IMapper mapper, ICartService service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    /// <summary>
    /// Devuelve carritos mediante filtros y paginado
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CartResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CartResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CartResponseDto>>))]
    public async Task<IActionResult> Get([FromQuery] CartQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var dtos = _mapper.Map<IEnumerable<CartResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<CartResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    /// <summary>
    /// Lista los detalles de un carrito
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CartDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CartDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CartDetailResponseDto>>))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        Expression<Func<Cart, bool>> filter = x => x.Id == id;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _service.GetById(id);

        var dto = _mapper.Map<CartDetailResponseDto>(entity);
        var response = new ApiResponse<CartDetailResponseDto>(data: dto);
        return Ok(response);
    }

    /// <summary>
    /// Crea un carrito de compras
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPost]
    [Route("")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CartResponseDto>))]
    public async Task<IActionResult> Create([FromBody] CartCreateRequestDto requestDto)
    {
        try
        {
            var entity = _mapper.Map<Cart>(requestDto);
            entity.CreatedBy = _tokenHelper.GetUserName();

            await _service.CreateCart(entity, requestDto.CraftIds!);

            var result = _mapper.Map<CartResponseDto>(entity);
            var response = new ApiResponse<CartResponseDto>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    /// <summary>
    /// Cancela el carrito de compra
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id:int}/Cancel")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CartResponseDto>))]
    public async Task<IActionResult> Cancel([FromRoute] int id)
    {
        Expression<Func<Cart, bool>> filter = x => x.Id == id;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _service.GetById(id);
        entity.Status = (short)CartStatus.Canceled;
        entity.LastModifiedBy = _tokenHelper.GetUserName();
        entity.LastModifiedDate = DateTime.Now;
        await _service.Update(entity);

        var dto = _mapper.Map<CartResponseDto>(entity);
        var response = new ApiResponse<CartResponseDto>(data: dto);
        return Ok(response);
    }
}