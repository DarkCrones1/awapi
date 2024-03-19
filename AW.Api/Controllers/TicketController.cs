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
using AW.Domain.Enumerations;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin-Customer")]
public class TicketController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITicketService _service;
    private readonly TokenHelper _tokenHelper;
    private readonly ICartService _cartService;

    public TicketController(IMapper mapper, ITicketService service, TokenHelper tokenHelper, ICartService cartService)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._cartService = cartService;
    }

    /// <summary>
    /// Devuelve tickets mediante filtro y paginado
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<TicketResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<TicketResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<TicketResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] TicketQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var dtos = _mapper.Map<IEnumerable<TicketResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<TicketResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    /// <summary>
    /// Lista los detalles del ticket
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<TicketDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<TicketDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<TicketDetailResponseDto>>))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        Expression<Func<Ticket, bool>> filter = x => x.Id == id;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _service.GetById(id);

        var dto = _mapper.Map<TicketDetailResponseDto>(entity);
        var response = new ApiResponse<TicketDetailResponseDto>(data: dto);
        return Ok(response);
    }

    /// <summary>
    /// Genera el ticket para un carrito pendiente
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<TicketResponseDto>))]
    public async Task<IActionResult> Create([FromBody] TicketCreateRequestDto requestDto)
    {
        var entity = _mapper.Map<Ticket>(requestDto);
        entity.CreatedBy = _tokenHelper.GetUserName();
        await _service.Create(entity);

        var cart = await _cartService.GetById(entity.CartId);
        cart.Status = (short)CartStatus.Pendding;

        var dto = _mapper.Map<TicketResponseDto>(entity);
        var response = new ApiResponse<TicketResponseDto>(data: dto);
        return Ok(response);
    }

    /// <summary>
    /// Cancela el ticket y el carrito
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id:int}/CancelTicket")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<TicketResponseDto>))]
    public async Task<IActionResult> CancelTicket([FromRoute] int id)
    {
        Expression<Func<Ticket, bool>> filter = x => x.Id == id;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _service.GetById(id);
        entity.Status = (short)TicketStatus.Canceled;
        entity.LastModifiedBy = _tokenHelper.GetUserName();
        entity.CloseTicket = DateTime.Now;
        entity.LastModifiedDate = DateTime.Now;
        
        await _service.Update(entity);

        var cart = await _cartService.GetById(entity.CartId);
        cart.Status = (short)CartStatus.Canceled;
        
        var dto = _mapper.Map<TicketResponseDto>(entity);
        var response = new ApiResponse<TicketResponseDto>(data: dto);
        return Ok(response);
    }
}