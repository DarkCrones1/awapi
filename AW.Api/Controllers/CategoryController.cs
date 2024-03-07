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
public class CategoryController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICategoryService _service;
    private readonly TokenHelper _tokenHelper;

    public CategoryController(IMapper mapper, ICategoryService service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    /// <summary>
    /// Devuelve categorias mediante filtros y paginado
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CategoryResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CategoryResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CategoryResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] CategoryQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var dtos = _mapper.Map<IEnumerable<CategoryResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<CategoryResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    /// <summary>
    /// Busca una categoria mediante su Id, devuelve información detallada
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CategoryResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CategoryResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CategoryResponseDto>>))]
    public async Task<IActionResult> GetAll([FromRoute] int id)
    {
        Expression<Func<Category, bool>> filter = x => x.Id == id;
        var existCategory = await _service.Exist(filter);

        if (!existCategory)
            return BadRequest("No existen coincidencias");

        var entity = await _service.GetById(id);
        var dto = _mapper.Map<CategoryResponseDto>(entity);
        var response = new ApiResponse<CategoryResponseDto>(data: dto);
        return Ok(response);
    }

    /// <summary>
    /// Crear Categorias
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPost]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CategoryResponseDto>))]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateRequestDto requestDto)
    {
        try
        {
            var entity = _mapper.Map<Category>(requestDto);
            entity.CreatedBy = _tokenHelper.GetUserName();
            await _service.Create(entity);
            var dto = _mapper.Map<CategoryResponseDto>(entity);
            var response = new ApiResponse<CategoryResponseDto>(data: dto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw new LogicBusinessException(ex);
        }
    }

    /// <summary>
    /// Actualiza una categoria
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPut]
    [Route("{id:int}")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CategoryResponseDto>))]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryUpdateRequestDto requestDto)
    {
        try
        {
            Expression<Func<Category, bool>> filter = x => x.Id == id;
            var existCategory = await _service.Exist(filter);

            if (!existCategory)
                return BadRequest("No se encontró el elemento que desea modificar");

            var newEntity = _mapper.Map<Category>(requestDto);
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
    /// Elimina de manera lógica una categoria
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
            Expression<Func<Category, bool>> filter = x => x.Id == id;
            var existCategory = await _service.Exist(filter);

            if (!existCategory)
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