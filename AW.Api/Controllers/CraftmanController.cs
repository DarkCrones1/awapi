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

namespace AW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CraftmanController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICraftmantService _service;
    private readonly TokenHelper _tokenHelper;

    public CraftmanController(IMapper mapper, ICraftmantService service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    /// <summary>
    /// Devuelve todos los Artesanos registrados mediante filtros y paginado
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CraftmanResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CraftmanResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CraftmanResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] CraftmanQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var dtos = _mapper.Map<IEnumerable<CraftmanResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<CraftmanResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    /// <summary>
    /// Actualiza la información del Artesano con su Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPut]
    [Route("{id:int}")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CraftmanResponseDto>))]
    public async Task<IActionResult> UpdateCraftmant([FromRoute] int id, [FromBody] CraftmanUpdateRequestDto requestDto)
    {
        try
        {
            Expression<Func<Craftman, bool>> filter = x => x.Id == id;
            var existCraftman = await _service.Exist(filter);

            if (!existCraftman)
                return BadRequest("No se encontró ningun Artesano");

            var newEntity = _mapper.Map<Craftman>(requestDto);
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

}