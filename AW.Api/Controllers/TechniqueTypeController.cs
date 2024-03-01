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
public class TechniqueTypeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICatalogBaseService<TechniqueType> _service;
    private readonly TokenHelper _tokenHelper;

    public TechniqueTypeController(IMapper mapper, ICatalogBaseService<TechniqueType> service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    /// <summary>
    /// Crea tipos de tecnicas que se usan para elaborar las artesanias
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPost]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<TechniqueTypeResponseDto>))]
    public async Task<IActionResult> CreateTechniqueType([FromBody] TechniqueTypeCreateRequestDto requestDto)
    {
        try
        {
            var entity = _mapper.Map<TechniqueType>(requestDto);
            entity.CreatedBy = _tokenHelper.GetUserName();
            await _service.Create(entity);
            var dto = _mapper.Map<TechniqueTypeResponseDto>(entity);
            var response = new ApiResponse<TechniqueTypeResponseDto>(data: dto);
            return Ok(response);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    /// <summary>
    /// Actualiza una tecnica
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPut]
    [Route("{id:int}")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<TechniqueTypeResponseDto>))]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TechniqueTypeUpdateRequestDto requestDto)
    {
        try
        {
            Expression<Func<TechniqueType, bool>> filter = x => x.Id == id;
            var existTechnique = await _service.Exist(filter);

            if (!existTechnique)
                return BadRequest("No se encontró el elemento que desea modificar");

            var newEntity = _mapper.Map<TechniqueType>(requestDto);
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
    /// Elimina de manera lógica una tecnica
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
            Expression<Func<TechniqueType, bool>> filter = x => x.Id == id;
            var existTechnique = await _service.Exist(filter);

            if (!existTechnique)
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