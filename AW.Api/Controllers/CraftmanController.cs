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
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin-Craftman")]
public class CraftmanController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ICraftmantService _service;
    private readonly TokenHelper _tokenHelper;
    private readonly ILocalStorageService _localService;

    public CraftmanController(IMapper mapper, IConfiguration configuration, ICraftmantService service, TokenHelper tokenHelper, ILocalStorageService localService)
    {
        this._mapper = mapper;
        this._configuration = configuration;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._localService = localService;
    }

    /// <summary>
    /// Devuelve todos los Artesanos registrados mediante filtros y paginado
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [AllowAnonymous]
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
    /// Lista los detalles del Artesano
    /// </summary>
    [HttpGet]
    [Route("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CraftmanDetailResponseDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<CraftmanDetailResponseDto>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<CraftmanDetailResponseDto>))]
    public async Task<IActionResult> GetCraftmanDetail([FromRoute] int id)
    {
        var entity = await _service.GetById(id);

        if (entity.Id <= 0)
            return NotFound();

        var dto = _mapper.Map<CraftmanDetailResponseDto>(entity);
        var response = new ApiResponse<CraftmanDetailResponseDto>(data: dto);
        return Ok(response);
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
    /// Sirve para subir una imagen de usuario
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPost]
    [Route("UploadImageProfile")]
    public async Task<IActionResult> UploadImageProfileLocal([FromForm] ImageCreateRequestDto requestDto)
    {
        try
        {
            var urlFile = await _localService.UploadAsync(requestDto.File, LocalContainer.Image_Profile, Guid.NewGuid().ToString());

            string url = $"{GetUrlBaseLocal((short)LocalContainer.Image_Profile)}{urlFile}";

            await _service.UpdateProfile(requestDto.EntityAssigmentId, url, _tokenHelper.GetUserName());

            return Ok();
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
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

            var dto = _mapper.Map<CraftmanResponseDto>(newEntity);
            var response = new ApiResponse<CraftmanResponseDto>(data: dto);
            return Ok(response);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    /// <summary>
    /// Actualiza la foto de perfil del Usuario
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPut]
    [Route("UpdateImageProfile")]
    public async Task<IActionResult> UpdateImageProfileLocal([FromForm] ImageCreateRequestDto requestDto)
    {
        try
        {
            Expression<Func<Craftman, bool>> filter = x => x.Id == requestDto.EntityAssigmentId;
            var existCraftman = await _service.Exist(filter);

            if (!existCraftman)
                return BadRequest("No se encontró ningun Artesano");

            var entity = await _service.GetById(requestDto.EntityAssigmentId);

            var urlFile = await _localService.EditFileAsync(requestDto.File, LocalContainer.Image_Profile, entity.ProfilePictureUrl!);

            string url = $"{GetUrlBaseLocal((short)LocalContainer.Image_Profile)}{urlFile}";

            await _service.UpdateProfile(requestDto.EntityAssigmentId, url, _tokenHelper.GetUserName());

            return Ok();
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    /// <summary>
    /// Elimina de manera lógica un Artesano y deshabilita su cuenta de usuario
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
            Expression<Func<Craftman, bool>> filter = x => x.Id == id;
            var existCraftman = await _service.Exist(filter);

            if (!existCraftman)
                return BadRequest("No se encontró ningun Artesano");

            var entity = await _service.GetById(id);
            entity.LastModifiedBy = _tokenHelper.GetUserName();
            entity.LastModifiedDate = DateTime.Now;
            entity.IsDeleted = true;
            entity.Id = id;
            entity.Status = (short)CraftmanStatus.Downed;

            await _service.DownedProfile(entity);
            return Ok(true);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    /// <summary>
    /// Elimina la imagen de perfil del usuario
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpDelete]
    [Route("{id:int}/DeleteImageProfile")]
    public async Task<IActionResult> DeleteImageProfile([FromRoute] int id)
    {
        try
        {
            Expression<Func<Craftman, bool>> filter = x => x.Id == id;
            var existCraftman = await _service.Exist(filter);

            if (!existCraftman)
                return BadRequest("No se encontró ningun Artesano");

            var entity = await _service.GetById(id);

            await _localService.DeteleAsync(LocalContainer.Image_Profile, entity.ProfilePictureUrl!);
            await _service.UpdateProfile(id, null!, _tokenHelper.GetUserName());
            return Ok(true);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

}