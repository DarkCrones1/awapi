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

    public CraftmanController(IMapper mapper, IConfiguration configuration, ICraftmantService service, TokenHelper tokenHelper, IAzureBlobStorageService fileService)
    {
        this._mapper = mapper;
        this._configuration = configuration;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._fileService = fileService;
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

    private string GetUrlBase(int type)
    {
        var url = type switch
        {
            1 => _configuration.GetValue<string>("DefaultValues:customerIdentificationAzureStorageBaseURL"),
            2 => _configuration.GetValue<string>("DefaultValues:customerProofAddressAzureStorageBaseURL"),
            3 => _configuration.GetValue<string>("DefaultValues:imageProfileAzureStorageBaseURL"),
            4 => _configuration.GetValue<string>("DefaultValues:customerDocuments"),
            _ => _configuration.GetValue<string>("DefaultValues:craftmanDocuments")
        };
        return url!;
    }

    private AzureContainer GetAzureContainer(int value)
    {
        return value switch
        {
            1 => AzureContainer.Customer_Identification,
            2 => AzureContainer.Customer_Proof_Address,
            3 => AzureContainer.Image_Profile,
            4 => AzureContainer.Customer_Other_Documents,
            _ => AzureContainer.Craftman_Documents
        };
    }

    [HttpPost]
    [Route("uploadImageProfile")]
    public async Task<IActionResult> UploadImageProfile([FromForm] CraftmanImageProfileCreateRequestDto requestDto)
    {
        var entity = _mapper.Map<AWDocument>(requestDto);

        // Upload File
        var urlFile = await _fileService.UploadAsync(requestDto.File!, AzureContainer.Image_Profile, Guid.NewGuid().ToString());

        // Register File
        entity.UrlDocument = $"{GetUrlBase((short)AzureContainer.Image_Profile)}{urlFile}";

        // Update Craftman information
        await _service.UpdateProfile(requestDto.CraftmanId, entity.UrlDocument);

        return Ok(entity.Id);
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