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
    private readonly ICustomerService _service;
    private readonly TokenHelper _tokenHelper;

    public CustomerController(IMapper mapper, ICustomerService service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
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