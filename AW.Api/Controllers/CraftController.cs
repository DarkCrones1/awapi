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
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin-Craftman")]
public class CraftController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ICraftService _service;
    private readonly TokenHelper _tokenHelper;
    private readonly ILocalStorageService _localService;

    public CraftController(IMapper mapper, IConfiguration configuration, ICraftService service, TokenHelper tokenHelper, ILocalStorageService localService)
    {
        this._mapper = mapper;
        this._configuration = configuration;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._localService = localService;
    }

    /// <summary>
    /// Devuelve artesanias mediante filtros y paginado
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CraftResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CraftResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CraftResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] CraftQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var dtos = _mapper.Map<IEnumerable<CraftResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<CraftResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    /// <summary>
    /// Lista los detalles de una artesania
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CraftDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CraftDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CraftDetailResponseDto>>))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var entity = await _service.GetById(id);

        if (entity.Id <= 0)
            return NotFound();

        var dto = _mapper.Map<CraftDetailResponseDto>(entity);
        var response = new ApiResponse<CraftDetailResponseDto>(data: dto);
        return Ok(response);
    }

    /// <summary>
    /// Crea una artesania
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CraftResponseDto>))]
    public async Task<IActionResult> CreateCraft([FromBody] CraftCreateRequestDto requestDto)
    {
        try
        {
            var entity = _mapper.Map<Craft>(requestDto);
            entity.CreatedBy = _tokenHelper.GetUserName();
            entity.CraftmanId = _tokenHelper.GetCraftmanId();
            await _service.CreateCraft(entity, requestDto.CategoryIds!, requestDto.TechniqueTypeIds!);

            var result = _mapper.Map<CraftResponseDto>(entity);
            var response = new ApiResponse<CraftResponseDto>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw new LogicBusinessException(ex);
        }
    }

    private string GetUrlBaseLocal(int type)
    {
        var url = type switch
        {
            1 => _configuration.GetValue<string>("DefaultValues:craftImageLocalStorageBaseUrl"),
            2 => _configuration.GetValue<string>("DefaultValues:ImageProfileLocalStorageBaseUrl"),
            3 => _configuration.GetValue<string>("DefaultValues:categoryImageLocalStorageBaseUrl"),
            4 => _configuration.GetValue<string>("DefaultValues:customerDocuments"),
            _ => _configuration.GetValue<string>("DefaultValues:craftmanDocuments"),
        };
        return url!;
    }

    private static LocalContainer GetLocalContainer(int value)
    {
        return value switch
        {
            1 => LocalContainer.Image_Craft,
            2 => LocalContainer.Image_Profile,
            3 => LocalContainer.Image_Category,
            4 => LocalContainer.Customer_Other_Documents,
            _ => LocalContainer.Craftman_Other_Documents
        };
    }

    /// <summary>
    /// Sirve para subir una imagen de artesania
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPost]
    [Route("UploadImageCraft")]
    public async Task<IActionResult> UploadImageCraftLocal([FromForm] ImageCreateRequestDto requestDto)
    {
        try
        {
            var urlFile = await _localService.UploadAsync(requestDto.File, LocalContainer.Image_Craft, Guid.NewGuid().ToString());

            string url = $"{GetUrlBaseLocal((short)LocalContainer.Image_Craft)}{urlFile}";

            await _service.UpdateProfile(requestDto.EntityAssigmentId, url, _tokenHelper.GetUserName());

            return Ok();
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    /// <summary>
    /// Actualiza una artesania mediante su Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPut]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CraftResponseDto>))]
    public async Task<IActionResult> UpdateCraft(int id, [FromBody] CraftUpdateRequestDto requestDto)
    {
        try
        {
            Expression<Func<Craft, bool>> filter = x => x.Id == id;
            var existCraft = await _service.Exist(filter);

            if (!existCraft)
                return BadRequest("No se encontró el elemento que desea modificar");

            var entity = _mapper.Map<Craft>(requestDto);
            entity.LastModifiedBy = _tokenHelper.GetUserName();
            entity.LastModifiedDate = DateTime.Now;
            entity.IsDeleted = false;
            entity.Id = id;

            await _service.UpdateCraft(entity, requestDto.CategoryIds!, requestDto.TechniqueTypeIds!);

            var result = _mapper.Map<CraftResponseDto>(entity);
            var response = new ApiResponse<CraftResponseDto>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw new LogicBusinessException(ex);
        }
    }

    /// <summary>
    /// Actualiza la imagen de la artesania
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPut]
    [Route("UpdateImageCraft")]
    public async Task<IActionResult> UpdateImageCraftLocal([FromForm] ImageCreateRequestDto requestDto)
    {
        try
        {
            Expression<Func<Craft, bool>> filter = x => x.Id == requestDto.EntityAssigmentId;
            var existCraft = await _service.Exist(filter);

            if (!existCraft)
                return BadRequest("No se encontró ninguna Artesania");

            var entity = await _service.GetById(requestDto.EntityAssigmentId);

            var urlFile = await _localService.EditFileAsync(requestDto.File, LocalContainer.Image_Craft, entity.CraftPictureUrl!);

            string url = $"{GetUrlBaseLocal((short)LocalContainer.Image_Craft)}{urlFile}";

            await _service.UpdateProfile(requestDto.EntityAssigmentId, url, _tokenHelper.GetUserName());

            return Ok();
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    /// <summary>
    /// Elimina de manera lógica una artesania
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
            Expression<Func<Craft, bool>> filter = x => x.Id == id;
            var existCraft = await _service.Exist(filter);

            if (!existCraft)
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

    /// <summary>
    /// Elimina la imagen de la artesania
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpDelete]
    [Route("{id:int}/DeleteImageCraft")]
    public async Task<IActionResult> DeleteImageCraft([FromRoute] int id)
    {
        try
        {
            Expression<Func<Craft, bool>> filter = x => x.Id == id;
            var existCraft = await _service.Exist(filter);

            if (!existCraft)
                return BadRequest("No se encontró ninguna Artesania");

            var entity = await _service.GetById(id);

            await _localService.DeteleAsync(LocalContainer.Image_Craft, entity.CraftPictureUrl!);
            await _service.UpdateProfile(id, null!, _tokenHelper.GetUserName());
            return Ok(true);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }
}