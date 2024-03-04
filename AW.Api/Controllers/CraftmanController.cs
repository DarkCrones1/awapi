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

namespace AW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CraftmanController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ICraftmantService _service;
    private readonly TokenHelper _tokenHelper;
    private readonly IAzureBlobStorageService _fileService;
    private readonly ILocalStorageService _localService;

    public CraftmanController(IMapper mapper, IConfiguration configuration, ICraftmantService service, TokenHelper tokenHelper, IAzureBlobStorageService fileService, ILocalStorageService localService)
    {
        this._mapper = mapper;
        this._configuration = configuration;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._fileService = fileService;
        this._localService = localService;
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

    [HttpPost]
    [Route("uploadImageProfileLocal")]
    public async Task<IActionResult> UploadImageProfileLocal([FromForm] CraftmanImageProfileCreateRequestDto requestDto)
    {
        try
        {
            var urlFile = await _localService.UploadAsync(requestDto.File, LocalContainer.Image_Profile, Guid.NewGuid().ToString());
    
            string url = $"{GetUrlBaseLocal((short)LocalContainer.Image_Profile)}{urlFile}";
    
            await _service.UpdateProfile(requestDto.CraftmanId, url);
    
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