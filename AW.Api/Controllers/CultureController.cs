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
using AW.Application.Services;

namespace AW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CultureController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICultureService _service;
    private readonly TokenHelper _tokenHelper;

    public CultureController(IMapper mapper, ICultureService service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    /// <summary>
    /// Devuelve culturas mediante filtros y paginado
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CultureResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CultureResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CultureResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] CultureQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var dtos = _mapper.Map<IEnumerable<CultureResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<CultureResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    /// <summary>
    /// Lista los detalles de una cultura
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CultureResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CultureResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CultureResponseDto>>))]
    public async Task<IActionResult> GetAll([FromRoute] int id)
    {
        Expression<Func<Culture, bool>> filter = x => x.Id == id;
        var existCulture = await _service.Exist(filter);

        if (!existCulture)
            return BadRequest("No existen coincidencias");

        var entity = await _service.GetById(id);
        var dto = _mapper.Map<CultureResponseDto>(entity);
        var response = new ApiResponse<CultureResponseDto>(data: dto);
        return Ok(response);
    }

    /// <summary>
    /// Crea Culturas
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CultureResponseDto>))]
    public async Task<IActionResult> Create([FromBody] CultureCreateRequestDto requestDto)
    {
        var entity = _mapper.Map<Culture>(requestDto);
        entity.CreatedBy = _tokenHelper.GetUserName();
        await _service.Create(entity);
        var dto = _mapper.Map<CultureResponseDto>(entity);
        var response = new ApiResponse<CultureResponseDto>(dto);
        return Ok(response);

    }


    /// <summary>
    /// Actualiza una cultura
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPut]
    [Route("{id:int}")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CultureResponseDto>))]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryUpdateRequestDto requestDto)
    {
        try
        {
            Expression<Func<Culture, bool>> filter = x => x.Id == id;
            var existCulture = await _service.Exist(filter);

            if (!existCulture)
                return BadRequest("No se encontró el elemento que desea modificar");

            var newEntity = _mapper.Map<Culture>(requestDto);
            newEntity.IsDeleted = false;
            newEntity.LastModifiedBy = _tokenHelper.GetUserName();
            newEntity.LastModifiedDate = DateTime.Now;
            newEntity.Id = id;
            await _service.Update(newEntity);
            return Ok(requestDto);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    /// <summary>
    /// Elimina de manera logica una cultura
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpDelete]
    [Route("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            Expression<Func<Culture, bool>> filter = x => x.Id == id;
            var existCulture = await _service.Exist(filter);

            if (!existCulture)
                return BadRequest("No se encontró el elemento que desea modificar");

            var oldEntity = await _service.GetById(id);
            oldEntity.IsDeleted = true;
            oldEntity.LastModifiedBy = _tokenHelper.GetUserName();
            oldEntity.LastModifiedDate = DateTime.Now;
            await _service.Update(oldEntity);
            return Ok(true);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }
}