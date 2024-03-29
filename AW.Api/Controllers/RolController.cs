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
using AW.Common.QueryFilters;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
public class RolController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICatalogBaseService<Rol> _service;
    private readonly ITokenHelperService _tokenHelper;

    public RolController(IMapper mapper, ICatalogBaseService<Rol> service, ITokenHelperService tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    /// <summary>
    /// Devuelve roles mediante filtro y paginado
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BaseCatalogResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<BaseCatalogResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<BaseCatalogResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] BaseCatalogQueryFilter filter)
    {
        var filters = _mapper.Map<Rol>(filter);
        var entities = await _service.GetPaged(filters);
        var dto = _mapper.Map<IEnumerable<BaseCatalogResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<BaseCatalogResponseDto>>(data: dto, meta: metaDataResponse);
        return Ok(response);
    }

    /// <summary>
    /// Crea un Rol
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<BaseCatalogResponseDto>))]
    public async Task<IActionResult> Create([FromBody] BaseCatalogCreateRequestDto requestDto)
    {
        var entity = _mapper.Map<Rol>(requestDto);
        await _service.Create(entity);
        var dto = _mapper.Map<BaseCatalogResponseDto>(entity);
        var response = new ApiResponse<BaseCatalogResponseDto>(data: dto);
        return Ok(response);
    }

    /// <summary>
    /// Actualiza un rol
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BaseCatalogUpdateRequestDto requestDto)
    {
        Expression<Func<Rol, bool>> filter = x => x.Id == id;
        var existRol = await _service.Exist(filter);
        if (!existRol)
            return BadRequest("No se encontró ningun Rol con ese id");

        var newEntity = _mapper.Map<Rol>(requestDto);
        newEntity.Id = id;
        newEntity.IsDeleted = false;
        newEntity.LastModifiedDate = DateTime.Now;
        newEntity.LastModifiedBy = _tokenHelper.GetUserName();
        await _service.Update(newEntity);
        return Ok(requestDto);
    }


    /// <summary>
    /// Elimina de manera logica un rol
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        Expression<Func<Rol, bool>> filter = x => x.Id == id;
        var existRol = await _service.Exist(filter);
        if (!existRol)
            return BadRequest("No se encontró ningun Rol con ese id");

        var entity = await _service.GetById(id);
        entity.IsDeleted = true;
        entity.LastModifiedDate = DateTime.Now;
        entity.LastModifiedBy = _tokenHelper.GetUserName();
        entity.Id = id;
        await _service.Update(entity);
        return Ok(true);
    }
}