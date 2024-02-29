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
public class CountryController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICrudService<Country> _service;
    private readonly TokenHelper _tokenHelper;

    public CountryController(IMapper mapper, ICrudService<Country> service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    /// <summary>
    /// Devuelve estados mediante filtros y paginado
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CountryResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CountryResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CountryResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] CountryQueryFilter filter)
    {
        var filters = _mapper.Map<Country>(filter);
        var entities = await _service.GetPaged(filters);
        var dtos = _mapper.Map<IEnumerable<CountryResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<CountryResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }
}