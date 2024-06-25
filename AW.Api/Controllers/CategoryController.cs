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
using AW.Common.Enumerations;

namespace AW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
public class CategoryController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ICategoryService _service;
    private readonly ITokenHelperService _tokenHelper;
    private readonly ILocalStorageService _localService;

    public CategoryController(IMapper mapper, IConfiguration configuration, ICategoryService service, ITokenHelperService tokenHelper, ILocalStorageService localService)
    {
        this._mapper = mapper;
        this._configuration = configuration;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._localService = localService;
    }

    /// <summary>
    /// Devuelve categorias mediante filtros y paginado
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [AllowAnonymous]
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
    /// Lista los detalles de una categoria
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:int}")]
    [AllowAnonymous]
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

    private string GetUrlBaseLocal(int type)
    {
        var url = type switch
        {
            1 => _configuration.GetValue<string>("DefaultValues:craftImageLocalStorageBaseUrl"),
            2 => _configuration.GetValue<string>("DefaultValues:ImageProfileCraftmanLocalStorageBaseUrl"),
            3 => _configuration.GetValue<string>("DefaultValues:ImageProfileCustomerLocalStorageBaseUrl"),
            4 => _configuration.GetValue<string>("DefaultValues:categoryImageLocalStorageBaseUrl"),
            5 => _configuration.GetValue<string>("DefaultValues:customerDocuments"),
            _ => _configuration.GetValue<string>("DefaultValues:craftmanDocuments"),
        };
        return url!;
    }

    private static LocalContainer GetLocalContainer(int value)
    {
        return value switch
        {
            1 => LocalContainer.Image_Craft,
            2 => LocalContainer.Image_Profile_Craftman,
            3 => LocalContainer.Image_Profile_Customer,
            4 => LocalContainer.Image_Category,
            5 => LocalContainer.Customer_Other_Documents,
            _ => LocalContainer.Craftman_Other_Documents
        };
    }

    /// <summary>
    /// Sirve para subir una imagen de categoria
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPost]
    [Route("UploadImageCategory")]
    public async Task<IActionResult> UploadImageCraftLocal([FromForm] ImageCreateRequestDto requestDto)
    {
        try
        {
            var urlFile = await _localService.UploadAsync(requestDto.File, LocalContainer.Image_Category, Guid.NewGuid().ToString());

            string url = $"{GetUrlBaseLocal((short)LocalContainer.Image_Category)}{urlFile}";

            await _service.UpdateProfile(requestDto.EntityAssigmentId, url, _tokenHelper.GetUserName());

            return Ok();
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    /// <summary>
    /// Actualiza la imagen de la categoria
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPut]
    [Route("UpdateImageCategory")]
    public async Task<IActionResult> UpdateImageCraftLocal([FromForm] ImageCreateRequestDto requestDto)
    {
        try
        {
            Expression<Func<Category, bool>> filter = x => x.Id == requestDto.EntityAssigmentId;
            var existCraft = await _service.Exist(filter);

            if (!existCraft)
                return BadRequest("No se encontró ninguna Artesania");

            var entity = await _service.GetById(requestDto.EntityAssigmentId);

            var urlFile = await _localService.EditFileAsync(requestDto.File, LocalContainer.Image_Craft, entity.CategoryPictureUrl!);

            string url = $"{GetUrlBaseLocal((short)LocalContainer.Image_Category)}{urlFile}";

            await _service.UpdateProfile(requestDto.EntityAssigmentId, url, _tokenHelper.GetUserName());

            return Ok();
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    [HttpDelete]
    [Route("{id:int}/DeleteImageCategory")]
    public async Task<IActionResult> DeleteImageCraft([FromRoute] int id)
    {
        try
        {
            Expression<Func<Category, bool>> filter = x => x.Id == id;
            var existCraft = await _service.Exist(filter);

            if (!existCraft)
                return BadRequest("No se encontró ninguna Artesania");

            var entity = await _service.GetById(id);

            await _localService.DeteleAsync(LocalContainer.Image_Category, entity.CategoryPictureUrl!);
            await _service.UpdateProfile(id, null!, _tokenHelper.GetUserName());
            return Ok(true);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }
}