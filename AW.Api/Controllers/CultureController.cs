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
using AW.Application.Services;

namespace AW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CultureController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly CatalogBaseService<Culture> _service;
    private readonly TokenHelper _tokenHelper;

    public CultureController(IMapper mapper, CatalogBaseService<Culture> service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CultureResponseDto>))]
    public async Task<IActionResult> Create([FromBody] CultureCreateRequestDto requestDto)
    {
        var entity = _mapper.Map<Culture>(requestDto);
        entity.CreatedBy = _tokenHelper.GetUserName();
        await _service.Create(entity);
        var dto  = _mapper.Map<CultureResponseDto>(entity);
        var response = new ApiResponse<CultureResponseDto>(dto);
        return Ok(response);
        
    }
}