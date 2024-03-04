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
public class CustomerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ICustomerService _service;
    private readonly TokenHelper _tokenHelper;
    private readonly ILocalStorageService _localService;

    public CustomerController(IMapper mapper, IConfiguration configuration,ICustomerService service, TokenHelper tokenHelper, ILocalStorageService localService)
    {
        this._mapper = mapper;
        this._configuration = configuration;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._localService = localService;
    }

    [HttpGet]
    [Route("")]
    [ActionName("GetCustomers")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CustomerResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CustomerResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CustomerResponseDto>>))]

    public async Task<IActionResult> GetCustomers([FromQuery] CustomerQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);

        var dtos = _mapper.Map<IEnumerable<CustomerResponseDto>>(entities);

        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );

        var response = new ApiResponse<IEnumerable<CustomerResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    /// <summary>
    /// Lista los detalles del cliente
    /// </summary>
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CustomerResponseDto>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<CustomerResponseDto>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<CustomerResponseDto>))]
    public async Task<IActionResult> GetCustomerDetail([FromRoute] int id)
    {
        var entity = await _service.GetById(id);

        if (entity.Id <= 0)
            return NotFound();

        var dto = _mapper.Map<CustomerResponseDto>(entity);
        var response = new ApiResponse<CustomerResponseDto>(data: dto);
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
    [Route("uploadImageProfileLocal")]
    public async Task<IActionResult> UploadImageProfileLocal([FromForm] ImageProfileCreateRequestDto requestDto)
    {
        try
        {
            var urlFile = await _localService.UploadAsync(requestDto.File, LocalContainer.Image_Profile, Guid.NewGuid().ToString());
    
            string url = $"{GetUrlBaseLocal((short)LocalContainer.Image_Profile)}{urlFile}";
    
            await _service.UpdateProfile(requestDto.CraftmanId, url, _tokenHelper.GetUserName());
    
            return Ok();
        }
        catch (Exception ex)
        {
            
            throw new LogicBusinessException(ex);
        }
    }

    /// <summary>
    /// Actualiza la información de un cliente
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <exception cref="LogicBusinessException"></exception>
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CustomerUpdateRequestDto requestDto)
    {
        try
        {
            Expression<Func<Customer, bool>> filter = x => x.Id == id;
            var existCustomer = await _service.Exist(filter);

            if (!existCustomer)
                return BadRequest("No existe ningun empleado con esa información");

            var newEntity = _mapper.Map<Customer>(requestDto);
            newEntity.IsDeleted = false;
            newEntity.Id = id;
            newEntity.LastModifiedBy = _tokenHelper.GetUserName();
            newEntity.LastModifiedDate = DateTime.Now;

            var customerAddress = _mapper.Map<CustomerAddress>(requestDto);
            customerAddress.CustomerId = id;
            customerAddress.IsDefault = true;
            customerAddress.Status = 1;
            newEntity.CustomerAddress.Add(customerAddress);

            await _service.Update(newEntity);

            var dto = _mapper.Map<CustomerResponseDto>(newEntity);
            var response = new ApiResponse<CustomerResponseDto>(data: dto);
            return Ok(response);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }
}